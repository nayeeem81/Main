using MailKit.Net.Smtp;
using Main.Infrastructure.DatabaseContext;
using Main.Model.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace OutboxEmailSender.WorkerService;

public class Worker: BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30); // Runs every 30 seconds

    public Worker (ILogger<Worker> logger,IServiceScopeFactory scopeFactory,IConfiguration configuration)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync (CancellationToken stoppingToken)
    {
        while ( !stoppingToken.IsCancellationRequested )
        {
            _logger.LogInformation ("Worker running at: {time}",DateTimeOffset.Now);

            try
            {
                // Create a temporary scope to use the Scoped DbContext safely
                using var scope = _scopeFactory.CreateScope ();
                var dbContext = scope.ServiceProvider.GetRequiredService<LogDbContext>();

                // 1. EF Core Query: Get unsent emails
                var pendingEmails = await dbContext.EmailOutboxMessages
                        .Where(e => e.ProcessedOnUtc == null)
                        .ToListAsync(stoppingToken);

                if ( pendingEmails.Any () )
                {
                    _logger.LogInformation ("Found {count} emails to send.",pendingEmails.Count);

                    foreach ( var email in pendingEmails )
                    {
                        // 2. Send the Email
                        await SendSmtpEmailAsync (email);

                        // 3. Update database record status
                        email.ProcessedOnUtc = DateTime.UtcNow;
                    }

                    // Save all updates to the database
                    _ = await dbContext.SaveChangesAsync (stoppingToken);
                }
            }
            catch ( Exception ex )
            {
                _logger.LogError (ex,"An error occurred while processing emails.");
            }

            // Wait for the next interval before running again
            await Task.Delay (_checkInterval,stoppingToken);
        }
    }

    private async Task SendSmtpEmailAsync (EmailOutboxMessage email)
    {
        var smtpSection = _configuration.GetSection("SmtpSettings");
        var message = new MimeMessage();

        var senderName = smtpSection["SenderName"] ?? string.Empty;
        var senderEmail = smtpSection["SenderEmail"];

        if (string.IsNullOrWhiteSpace(senderEmail))
        {
            _logger.LogWarning("SMTP SenderEmail missing in configuration. Skipping email send for recipient '{recipient}'", email.ReceiverEmail);
            return;
        }

        message.From.Add(new MailboxAddress(senderName, senderEmail));

        var recipientEmail = email.ReceiverEmail?.ToString();
        if (string.IsNullOrWhiteSpace(recipientEmail))
        {
            _logger.LogWarning("Recipient email missing for Outbox message. Skipping.");
            return;
        }

        message.To.Add(new MailboxAddress("Store User", recipientEmail));

        message.Subject = email.Subject?.ToString() ?? string.Empty;

        var body  = email.Body?.ToString() ?? string.Empty;

        message.Body = new TextPart ("html")
        {
            Text = body
        };

        using var client = new SmtpClient();

        // Validate SMTP host and port before calling ConnectAsync to avoid CS8604
        var server = smtpSection["Server"];
        if (string.IsNullOrWhiteSpace(server))
        {
            _logger.LogWarning("SMTP Server missing in configuration. Skipping email send for recipient '{recipient}'", email.ReceiverEmail);
            return;
        }

        // Ensure port string is non-null before parsing
        var portStr = smtpSection["Port"] ?? string.Empty;
        if (!int.TryParse(portStr, out var port))
        {
            _logger.LogWarning("SMTP Port is missing or invalid in configuration. Using default port 25.");
            port = 25;
        }

        try
        {
            await client.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to SMTP server '{server}'", server);
            return;
        }

        var username = smtpSection["Username"];
        var password = smtpSection["Password"];

        // Only authenticate when both username and password are present (non-null/non-whitespace)
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            // Use local non-null asserted variables so the compiler knows values are non-null here.
            var nonNullUsername = username!;
            var nonNullPassword = password!;

            try
            {
                await client.AuthenticateAsync(nonNullUsername, nonNullPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMTP authentication failed for user '{user}'", username);
                await client.DisconnectAsync(true);
                return;
            }
        }

        try
        {
            _ = await client.SendAsync(message);
            _logger.LogInformation("Email sent successfully to {recipient}", email.ReceiverEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to '{recipient}'", recipientEmail);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
