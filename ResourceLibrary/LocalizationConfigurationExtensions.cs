using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ResourceLibrary;

public static class LocalizationConfigurationExtensions
{
    private static readonly CultureInfo[] SupportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("bn-BD")
    };
    
    public static IServiceCollection AddCustomLocalization ( this IServiceCollection services )
    {
        services.AddLocalization ( );

        services.AddControllersWithViews ( )
            .AddViewLocalization ( )
            .AddDataAnnotationsLocalization ( options =>
            {
                options.DataAnnotationLocalizerProvider = ( type,factory ) =>
                    factory.Create ( typeof ( SharedResource ) );
            } );

        return services;
    }

    public static IApplicationBuilder UseCustomLocalization ( 
        this IApplicationBuilder app )
    {
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = SupportedCultures,
            SupportedUICultures = SupportedCultures
        };

        localizationOptions.RequestCultureProviders = new           List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider(),
            new AcceptLanguageHeaderRequestCultureProvider()
        };

        app.UseRequestLocalization ( localizationOptions );

        return app;
    }
}
