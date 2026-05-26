
using Main.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;    

namespace Main.Services;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServiceDependencies ( 
                                this IServiceCollection services,
                                IConfiguration configuration )
    {

        
        var smtpSection = configuration.GetSection("SmtpSettings");


        services
            .AddFluentEmail ( smtpSection["SenderEmail"],smtpSection["SenderName"] )
            .AddSmtpSender ( new SmtpClient ( smtpSection["Server"] )
            {
                Port = int.Parse ( smtpSection["Port"] ?? "587" ),
                Credentials = new NetworkCredential ( smtpSection["User"],smtpSection["Pass"] ),
                EnableSsl = true
            } );


        services.AddTransient<IEmailSender,EmailSenderService> ( );


        services.AddMemoryCache ( options =>
        {

            options.SizeLimit = 1024;

            options.CompactionPercentage = 0.25;

        } );


        services.AddSession ( options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes ( 20 );

            options.Cookie.HttpOnly = true;

            options.Cookie.IsEssential = true; // Required to work without GDPR consent
        } );



        services.AddInfrastructureServices ( configuration );

        //Gives update about database exceptions in development environment 
        services.AddDatabaseDeveloperPageExceptionFilter ( );

        //Register Main Services
        services.AddScoped<IAccountService,AccountService> ( );
        services.AddScoped<IAdminPostService,AdminPostService> ( );
        services.AddScoped<IProductService,ProductService> ( );
        services.AddScoped<IPageService,PageService> ( );
        

        return services;
    }
}
