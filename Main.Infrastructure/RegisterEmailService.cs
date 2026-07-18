using Main.Infrastructure.CrosscuttingHelperServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Main.Infrastructure;

public static class RegisterEmailService
{
    public static IServiceCollection AddEmailService (this IServiceCollection services,
    IConfiguration configuration)
    {
        var smtpSection = configuration.GetSection("SmtpSettings");

        _ = services.AddFluentEmail (smtpSection["SenderEmail"],smtpSection["SenderName"])
                    .AddSmtpSender (smtpSection["Server"],int.Parse (smtpSection["Port"] ?? "587"),
                     smtpSection["Username"],smtpSection["Password"]);

        _ = services.AddScoped<IEmailSenderService,EmailSenderService> ();

        return services;
    }
}
