using Main.IRepository;
using Main.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class RegisterRepositoryExtensions
{
    public static IServiceCollection AddRepository (

    this IServiceCollection services,IConfiguration configuration)

    {
        _ = services.AddScoped<IApplicationUserRepository,ApplicationUserRepository> ();

        _ = services.AddScoped<ITenantRepository,TenantRepository> ();

        _ = services.AddScoped<ITenantInvitationRepository,TenantInvitationRepository> ();

        _ = services.AddScoped<ITenantUserRepository,TenantUserRepository> ();

        _ = services.AddScoped<IAdminPostRepository,AdminPostRepository> ();

        _ = services.AddScoped<IProductRepository,ProductRepository> ();

        _ = services.AddScoped<IPageRepository,PageRepository> ();

        _ = services.AddScoped<IPanelRepository,PanelRepository> ();

        return services;

    }
}

