using Main.Common;
using Main.Common.Models;
using Main.Infrastructure.ICrosscuttingServices;
using Main.IRepository;
using Main.Model.Identity;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public class EmailSenderService: IEmailSenderService
{
    private readonly IApplicationUserRepository _userRepositry;
    private readonly IEmailOutboxRepository _emailOutboxRepository;

    public EmailSenderService ()
    {
    }

    public EmailSenderService (

        IApplicationUserRepository userRepositry,
        IEmailOutboxRepository emailOutboxRepository)
    {

        _userRepositry = userRepositry;
        _emailOutboxRepository = emailOutboxRepository;
    }

    public async Task<string> SendEmailAsync (string userId)
    {
        ApplicationUser? identityUser = await _userRepositry.FindByNameIdAsync ( userId );

        await SendEmailAsync (
            identityUser?.Email != null ? identityUser.Email : "",
            "Account Unlocked",
            "Your account has been manually unlocked by an administrator. You may now log in."
        );

        return identityUser?.UserName ?? string.Empty;
    }


    public async Task SendEmailAsync (string email,string subject,string htmlMessage)
    {
        // Save the email to the outbox
        _ = await _emailOutboxRepository.SaveChangesAsync (email,subject,htmlMessage);
    }

    public async Task SendEmailVerificationAsync (VerifyDataModel verifyEmailDataModel)
    {
        string template
            = @"
            <html>
            <body>
                <p>Hi {{ Name }},</p>
                <p>Please click the link below to verify your email:</p>
                <p>
                    <a href='{{ LinkUrl }}' style='color: #007bff; text-decoration: underline;'>
                        Verify Email
                    </a>
                </p>
            </body>
            </html>";

        string populatedTemplate = template
                    .Replace("{{ Name }}", verifyEmailDataModel.UserName)
                    .Replace("{{ LinkUrl }}", verifyEmailDataModel.VerifyLink ?? string.Empty);

        await SendEmailAsync (
            verifyEmailDataModel.Email,
            verifyEmailDataModel.Subject,
            populatedTemplate);
    }


    public async Task SendResetPasswordEmailAsync (ResetDataModel resetEmailDataModel)
    {
        string template
            = @"
            <html>
            <body>
                <p>Hi {{ Name }},</p>
                <p>Please click the link below to reset your password:</p>
                <p>
                    <a href='{{ LinkUrl }}' style='color: #007bff; text-decoration: underline;'>
                        Reset Password  
                    </a>
                </p>
            </body>
            </html>";

        string populatedTemplate = template
                    .Replace("{{ Name }}", resetEmailDataModel.UserName)
                    .Replace("{{ LinkUrl }}", resetEmailDataModel.ResetLink ?? string.Empty);

        await SendEmailAsync (
            resetEmailDataModel.Email,
            resetEmailDataModel.Subject,
            populatedTemplate);
    }

    public async Task SendEmailAsync (string to,
    string subject,string body,CancellationToken ct = default)
    {
        await SendEmailAsync (to,subject,body);
    }
}
