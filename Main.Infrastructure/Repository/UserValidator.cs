using Domain.Model;

using Microsoft.AspNetCore.Identity;

namespace Main.Infrastructure;

public class TenantAwareUserValidator: UserValidator<ApplicationUser>
{
    public TenantAwareUserValidator ( )
    {
    }

    public override async Task<IdentityResult> ValidateAsync ( UserManager<ApplicationUser> manager,ApplicationUser user )
    {
        // First run standard validations
        IdentityResult result = await base.ValidateAsync(manager, user);

        if ( !result.Succeeded )
        {
            result = IdentityResult.Success;
            return result;
        }


        var owner = await manager.FindByNameAsync ( user.UserName != null ? user.UserName : "" );

        if ( owner == null )
        {
            List<IdentityError> errors = [];
            return IdentityResult.Failed ( errors.ToArray ( ) );
        }

        var ownerId = await manager.GetUserIdAsync ( owner );
        var userId = await manager.GetUserIdAsync ( user );

        if ( owner != null && owner.TenantId == user.TenantId
                           && string.Equals ( ownerId,userId ) )
        {
            result = IdentityResult.Success;
            return result;
        }

        return result;
    }
}
