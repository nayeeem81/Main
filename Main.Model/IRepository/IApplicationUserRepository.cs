using Domain.Model;
using System.Security.Claims;
namespace Main.IRepository;

public interface IApplicationUserRepository
{
    Task AddToRoleAsync (ApplicationUser applicationUser,string roleName);

    Task<ApplicationUser?> FindByEmailAsync (string email);

    Task<bool> PasswordSignInAsync (ApplicationUser applicationUser,string password,bool isPersistent,bool lockoutFailure);

    Task<bool> CreateAsync (ApplicationUser userIdentityEntity,string password);

    Task<bool> ChangePasswordAsync (string email,string password,string rePassword);

    Task<string?> GenerateEmailConfirmationTokenAsync (ApplicationUser user);

    Task<bool> ConfirmEmailAsync (ApplicationUser userIdentity,string token);

    Task<List<string>?> GetRolesAsync (string email,string tenantId);

    Task<ApplicationUser?> FindByNameIdAsync (string id);

    Task<bool> ResetAccessFailedCountAsync (ApplicationUser user);

    Task<bool> SetLockoutEndDateAsync (ApplicationUser user);

    Task<List<ApplicationUser>?> ApplicationUsers ();

    Task<bool> IsEmailConfirmedAsync (string email);

    Task<bool> PasswordSignInAsync
    (string userName,string password,bool isPersistent,bool lockoutOnFailure);

    Task AddClaimAsync (ApplicationUser applicationUser,Claim claimType);

    Task SignOutAsync ();

    Task<string> GeneratePasswordResetTokenAsync (string email);

    Task<bool> ResetPasswordAsync (string email,string token,string confirmPassword);
}
