using Main.Infrastructure;
using Main.WebAppCore.Tenant;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Main.WebAppCore.Middleware;

public class TenantRoleHandler: AuthorizationHandler<TenantRoleRequirement>
{
    private readonly ITenantSetter _tenantSetter;

    public TenantRoleHandler (ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    protected override Task HandleRequirementAsync (AuthorizationHandlerContext context,TenantRoleRequirement requirement)
    {
        var user = context.User;

        var tokenTenantId = user.FindFirst("tenant_id")?.Value;
        var resolvedTenantId = _tenantSetter.CurrentTenantId;
        var loggedUserId = user?.FindFirst
        (ClaimTypes.NameIdentifier)?.Value ?? "";

        if ( tokenTenantId == resolvedTenantId.ToString () &&
        context.User.IsInRole ("User") )
        {
            // Validate "IdentityId:TenantId:RoleName" claim after Login success
            var expectedClaimValue =
            $"{loggedUserId}:{resolvedTenantId}:{requirement.AllowedRole}";

            bool result = false;

            context.User.Claims.ToList ()
            .ForEach (tenantClaim =>
            {
                if ( tenantClaim.Type == "TenantRole" &&
                tenantClaim.Value == expectedClaimValue )
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
        else if ( context.User.IsInRole ("GlobalAdmin") )
        {
            context.Succeed (requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}