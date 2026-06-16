
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Services;

public static class RegisterEmailService
{
    public static IServiceCollection AddEmailService (
                                this IServiceCollection services,
                                IConfiguration configuration )
    {


        var smtpSection = configuration.GetSection("SmtpSettings");


        services.AddFluentEmail ( smtpSection["SenderEmail"],smtpSection["SenderName"] )

                .AddSmtpSender (
                        smtpSection["Server"],
                        int.Parse ( smtpSection["Port"] ?? "587" ),
                        smtpSection["Username"],
                        smtpSection["Password"]
                 );




        services.AddTransient<IEmailSender,EmailSenderService> ( );


        return services;
    }
}
