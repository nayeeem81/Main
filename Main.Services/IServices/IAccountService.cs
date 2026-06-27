using DataTransferModel;

using Microsoft.AspNetCore.Identity;

namespace Main.Services;

public interface IAccountService
{
    Task<IdentityResult> CreateApplicationUserAccount (UserAccountDataModel userAccountDataModel);

    Task<ApplicationUserDataModel?> GetApplicationUser (string email);

    Task<SignInResult> AuthenticateUser (string email,string password);

    Task<bool> ChangePasswordAsync (string email,string password,string rePassword);

    Task<bool> UnlockUser (string userId);

    Task<List<ApplicationUserDataModel>?> Users ();

    Task<string?> GetEmailVerifyToken (string email);

    Task<bool> CreateApplicationUser (string email,string token);

    Task GetUserClaims (string email,string tenantId);

    Task<ApplicationUserDataModel?> FindByEmailAsync (string email);

    Task<bool> IsEmailConfirmedAsync (string email);

    Task<bool> PasswordSignInAsync
    (string userName,string password,bool isPersistent,bool lockoutOnFailure);

    Task SignOutAsync ();

    Task<string> GeneratePasswordResetTokenAsync (string email);

    Task<bool> ResetPasswordAsync (string email,string token,string confirmPassword);

}