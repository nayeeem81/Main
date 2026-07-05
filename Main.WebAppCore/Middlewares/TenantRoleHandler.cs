using Main.Infrastructure;
using Main.WebAppCore.Tenant;
using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Middleware;

public class TenantRoleHandler: AuthorizationHandler<TenantRoleRequirement>
{
    private readonly ITenantContext _tenantContext;
    private readonly ITenantSetter _tenantSetter;

    public TenantRoleHandler (ITenantContext tenantContext,ITenantSetter tenantSetter)
    {
        _tenantContext = tenantContext;
        _tenantSetter = tenantSetter;
    }

    protected override Task HandleRequirementAsync (AuthorizationHandlerContext context,
    TenantRoleRequirement requirement)
    {
        if ( context.User.IsInRole ("GlobalAdmin") )
        {
            context.Succeed (requirement);
            return Task.CompletedTask;
        }

        var currentTenantId = _tenantSetter.CurrentTenantId;
        var currentUserId = _tenantContext.ApplicationUserId;

        if ( string.IsNullOrEmpty (currentTenantId) )
        {
            return Task.CompletedTask;
        }

        // Validate against the formatted "IdentityId:TenantId:RoleName" claim we made after Login success

        var expectedClaimValue =
        $"{currentUserId}:{currentTenantId}:{requirement.AllowedRole}";

        if ( context.User.IsInRole ("User") )
        {
            bool result = false;

            context.User.Claims.ToList ().ForEach (tenantClaim =>
            {
                if ( tenantClaim.Type == "TenantRole" && tenantClaim.Value == expectedClaimValue )
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            });

            if ( result )
            {
                context.Succeed (requirement);
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }
}