using Domain.Model;
using Main.Infrastructure;
using Main.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Main.Repository;

public class ApplicationUserRepository: IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public ApplicationUserRepository (UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<bool> AddToRoleAsync (string email,string roleName)
    {
        ApplicationUser? applicationUser = await FindByEmailAsync (email);

        var result = await _userManager.AddToRoleAsync (applicationUser!,roleName);

        return result.Succeeded == true;
    }

    public async Task<bool> AddToTenantRoleAsync (string email,string tenantId,string roleName)
    {
        ApplicationUser? applicationUser = await FindByEmailAsync (email);

        TenantUser userTenant = new ()
        {
            TenantId = tenantId,
            UserId = applicationUser!.Id,
            TenantRole = roleName
        };

        _ = _context.TenantUsers.Add (userTenant);
        var result = await _context.SaveChangesAsync ();
        return result > 0;
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

    public async Task<bool> PasswordSignInAsync (string userName,string password,bool isPersistent,bool lockoutFailure)
    {
        ApplicationUser? applicationUser = _context.ApplicationUsers.FirstOrDefault<ApplicationUser> (a => a.UserName == userName);


        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync (
            applicationUser!,
            password,
            isPersistent,
            lockoutOnFailure: lockoutFailure);

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

    public async Task<string?> GenerateEmailConfirmationTokenAsync (string email)
    {
        ApplicationUser?  applicationUser = await _userManager.FindByEmailAsync ( email );

        if ( applicationUser == null )
        {
            return "";
        }

        string? code = await _userManager.GenerateEmailConfirmationTokenAsync (applicationUser);

        return code;
    }

    public async Task<bool> ConfirmEmailAsync (string email,string token)
    {
        ApplicationUser?  applicationUser = await _userManager.FindByEmailAsync ( email );

        if ( applicationUser == null )
        {
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync (applicationUser,token);

        return result.Succeeded == true;
    }

    public async Task<List<string>> GetRolesAsync (string email)
    {
        ApplicationUser? user = await FindByEmailAsync(email);

        if ( user == null )
        {
            return new List<string> ();
        }

        var roles = await _userManager.GetRolesAsync (user);

        if ( roles == null )
        {
            return new List<string> ();
        }

        List<string> listRoles = [];

        if ( roles != null && roles.Any () )
        {
            listRoles.Add (roles[0]);
            return listRoles;
        }

        return new List<string> ();
    }

    public async Task<List<string>> GetTenantRolesAsync (string email,string tenantId)
    {
        ApplicationUser? user = await FindByEmailAsync(email);

        if ( user == null )
        {
            return new List<string> ();
        }

        List<TenantUser> userTenants = _context.TenantUsers.Where<TenantUser>
        (a => a.TenantId == tenantId && a.UserId == user.Id).ToList();

        if ( userTenants == null )
        {
            return new List<string> ();
        }

        var listTenantRoles = new List<string> ();

        if ( userTenants.Any () )
        {
            userTenants.ForEach (tenantUserRole =>
            {
                if ( !string.IsNullOrEmpty (tenantUserRole.TenantRole) )
                {
                    listTenantRoles.Add (tenantUserRole.TenantRole);
                }
            });
            return listTenantRoles.ToList ();
        }
        return new List<string> ();
    }

    public async Task<bool> SetLockoutEndDateAsync (string email)
    {
        ApplicationUser?  applicationUser = await _userManager.FindByEmailAsync (email);

        if ( applicationUser == null )
        {
            return false;
        }

        IdentityResult result = await _userManager.SetLockoutEndDateAsync(applicationUser, null);

        return result.Succeeded == true;
    }

    public async Task<bool> ResetAccessFailedCountAsync (string email)
    {
        ApplicationUser? applicationUser = await _userManager.FindByEmailAsync (email);

        if ( applicationUser == null )
        {
            return false;
        }

        IdentityResult result = await _userManager.ResetAccessFailedCountAsync(applicationUser);

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

    public async Task AddClaimAsync (string email,Claim claimType)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync (email);

        if ( user != null )
        {
            _ = await _userManager.AddClaimAsync (user,claimType);
        }
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
