using Domain.Model;
using System.Security.Claims;
namespace Main.IRepository;

public interface IApplicationUserRepository
{
    Task<bool> AddToRoleAsync (string email,string roleName);

    Task<bool> AddToTenantRoleAsync (string email,Guid tenantId,string roleName);

    Task<ApplicationUser?> FindByEmailAsync (string email);

    Task<bool> PasswordSignInAsync (string userName,string password,bool isPersistent,bool lockoutFailure);

    Task<bool> CreateAsync (ApplicationUser userIdentityEntity,string password);

    Task<bool> ChangePasswordAsync (string email,string password,string rePassword);

    Task<string?> GenerateEmailConfirmationTokenAsync (string email);

    Task<bool> ConfirmEmailAsync (string email,string token);

    Task<List<string>> GetRolesAsync (string email);

    Task<string> GetTenantRolesAsync (string email,Guid tenantId);

    Task<ApplicationUser?> FindByNameIdAsync (string id);

    Task<bool> ResetAccessFailedCountAsync (string email);

    Task<bool> SetLockoutEndDateAsync (string email);

    Task<List<ApplicationUser>?> ApplicationUsers ();

    Task<bool> IsEmailConfirmedAsync (string email);

    Task AddClaimAsync (string email,Claim claimType);

    Task SignOutAsync ();

    Task<string> GeneratePasswordResetTokenAsync (string email);

    Task<bool> ResetPasswordAsync (string email,string token,string confirmPassword);
}
