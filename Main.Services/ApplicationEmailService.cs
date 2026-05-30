
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;    

namespace Main.Services;

public static class ApplcationEmailServices
{
    public static IServiceCollection AddEmailService ( 
                                this IServiceCollection services,
                                IConfiguration configuration )
    {

        
        var smtpSection = configuration.GetSection("SmtpSettings");


        services.AddFluentEmail ( smtpSection["SenderEmail"], smtpSection["SenderName"] )
                
                .AddSmtpSender (
                        smtpSection["Server"],
                        int.Parse ( smtpSection["Port"] ?? "587" ),
                        smtpSection["User"],
                        smtpSection["Pass"]
                 );


        services.AddTransient<IEmailSender,EmailSenderService> ( );
        

        return services;
    }
}
