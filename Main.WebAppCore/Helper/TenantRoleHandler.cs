using Main.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace WebAppCore.Helper;

public class TenantRoleHandler: AuthorizationHandler<TenantRoleRequirement>
{
    private readonly ITenantSetter _tenantSetter;
    public TenantRoleHandler (IHttpContextAccessor httpContextAccessor,
        ITenantSetter tenantSetter)
    {
        _tenantSetter = tenantSetter;
    }

    protected override Task HandleRequirementAsync (AuthorizationHandlerContext context,TenantRoleRequirement requirement)
    {
        if ( context.User.IsInRole ("GlobalAdmin") )
        {
            context.Succeed (requirement);
            return Task.CompletedTask;
        }

        var currentTenantId = _tenantSetter.CurrentTenantId;

        if ( string.IsNullOrEmpty (currentTenantId) )
        {
            return Task.CompletedTask; // No tenant context found in URL
        }

        // 3. Validate against the formatted "TenantId:RoleName" claim we made earlier
        var expectedClaimValue = $"{currentTenantId}:{requirement.AllowedRole}";

        if ( context.User.HasClaim (c => c.Type == "TenantRole" && c.Value == expectedClaimValue) )
        {
            context.Succeed (requirement);
        }

        return Task.CompletedTask;
    }
}