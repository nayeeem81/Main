using Main.Infrastructure;
using Main.Services;
using Microsoft.AspNetCore.Authorization;

namespace Main.WebAppCore.Tenant;

public class TenantRoleHandler: AuthorizationHandler<TenantRoleRequirement>
{
    private readonly ITenantContext _tenantContext;
    private readonly ITenantSetter _tenantSetter;

    public TenantRoleHandler (ITenantContext tenantContext,ITenantSetter tenantSetter)
    {
        _tenantContext = tenantContext;
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
        var currentUserId = _tenantContext.IdentityId;

        if ( string.IsNullOrEmpty (currentTenantId) )
        {
            return Task.CompletedTask;
        }

        // 3. Validate against the formatted "IdentityId:TenantId:RoleName" claim we made earlier
        var expectedClaimValue = $"{currentUserId}:{currentTenantId}:{requirement.AllowedRole}";

        if ( context.User.IsInRole ("User") &&
        context.User.HasClaim (c => c.Type == "TenantRole" && c.Value == expectedClaimValue) )
        {
            context.Succeed (requirement);
        }

        return Task.CompletedTask;
    }
}