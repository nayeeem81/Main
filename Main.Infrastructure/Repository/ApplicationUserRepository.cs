using Domain.Model;
using Main.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Main.Repository;

public class ApplicationUserRepository: IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ApplicationUserRepository (UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task AddToRoleAsync (ApplicationUser applicationUser,string roleName)
    {
        _ = await _userManager.AddToRoleAsync (applicationUser,"User");
    }

    public async Task<ApplicationUser?> FindByEmailAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _userManager.FindByEmailAsync ( email );

        return applicationUser;
    }

    public async Task<ApplicationUser?> FindByNameIdAsync (string id)
    {
        ApplicationUser?  applicationUser
            = await _userManager.FindByIdAsync ( id );

        return applicationUser;
    }

    public async Task<bool> PasswordSignInAsync (ApplicationUser applicationUser,string password,bool isPersistent,bool lockoutFailure)
    {
        var result = await _signInManager.PasswordSignInAsync ( applicationUser, password, isPersistent, lockoutOnFailure: lockoutFailure);

        if ( result.Succeeded )
        {
            return true;
        }

        return false;
    }

    public async Task<bool> CreateAsync (ApplicationUser userIdentityEntity,
    string password)
    {
        var result = await _userManager.CreateAsync (userIdentityEntity, password);

        return result.Succeeded == true;
    }

    public async Task<bool> ChangePasswordAsync (string email,string password,string rePassword)
    {
        ApplicationUser?  applicationUser = await _userManager.FindByEmailAsync ( email );

        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(applicationUser, password, rePassword);

        return result.Succeeded == true;
    }

    public async Task<string?> GenerateEmailConfirmationTokenAsync (ApplicationUser user)
    {
        string? code = await _userManager.GenerateEmailConfirmationTokenAsync (user);

        return code;
    }

    public async Task<bool> ConfirmEmailAsync (ApplicationUser userIdentity,string token)
    {
        var result = await _userManager.ConfirmEmailAsync (userIdentity,token);

        return result.Succeeded == true;
    }

    public async Task<List<string>?> GetRolesAsync (string email,string tenantId)
    {
        ApplicationUser? user = await FindByEmailAsync(email);

        if ( user == null )
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync (user);

        if ( roles == null )
        {
            return null;
        }

        List<string>? listRoles = [];

        if ( roles != null && roles.Any () && roles[0] == "GlobalAdmin" )
        {
            listRoles.Add (roles[0]);
            return listRoles;
        }

        List<UserTenant> userTenants = user.UserTenants.ToList<UserTenant>();

        if ( userTenants == null )
        {
            return null;
        }

        if ( userTenants.Any () )
        {
            List<UserTenant> currentUserRoles = userTenants.Where<UserTenant>
            (a => a.TenantId == tenantId).ToList ();

            if ( currentUserRoles != null && currentUserRoles.Any ()
            && currentUserRoles.Count > 0 )
            {
                currentUserRoles.ForEach (tenantUserRole =>
                {
                    if ( !string.IsNullOrEmpty (tenantUserRole.TenantRole) )
                    {
                        listRoles.Add (tenantUserRole.TenantRole);
                    }
                });
            }
        }
        return listRoles;
    }

    public async Task<bool> SetLockoutEndDateAsync (ApplicationUser user)
    {
        IdentityResult result = await _userManager.SetLockoutEndDateAsync(user, null);

        return result.Succeeded == true;
    }

    public async Task<bool> ResetAccessFailedCountAsync (ApplicationUser user)
    {
        IdentityResult result = await _userManager.ResetAccessFailedCountAsync(user);

        return result.Succeeded == true;
    }

    public async Task<List<ApplicationUser>?> ApplicationUsers ()
    {
        List<ApplicationUser> identityUsers = await _userManager.Users.ToListAsync<ApplicationUser>();

        return identityUsers.ToList ();
    }

    public async Task<bool> IsEmailConfirmedAsync (string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync (email);

        if ( user != null )
        {
            var result = await _userManager.IsEmailConfirmedAsync (user);
            return result;
        }

        return false;
    }

    public async Task<bool> PasswordSignInAsync
    (string userName,string password,bool isPersistent,bool lockoutOnFailure)
    {
        var result = await _signInManager.PasswordSignInAsync (userName,password,isPersistent,lockoutOnFailure);

        return result.Succeeded == true;
    }

    public async Task AddClaimAsync (ApplicationUser applicationUser,Claim claimType)
    {
        _ = await _userManager.AddClaimAsync (applicationUser,claimType);
    }

    public async Task SignOutAsync ()
    {
        await _signInManager.SignOutAsync ();
    }

    public async Task<string> GeneratePasswordResetTokenAsync (string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync (email);

        if ( user != null )
        {
            string result = await _userManager.GeneratePasswordResetTokenAsync(user);
            return result;
        }

        return string.Empty;
    }

    public async Task<bool> ResetPasswordAsync (string email,string token,string confirmPassword)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync (email);

        if ( user != null )
        {
            _ = await _userManager.ResetPasswordAsync (user,token,confirmPassword);
            return true;
        }

        return false;
    }
}
