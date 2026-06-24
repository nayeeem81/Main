using DataTransferModel;

using Domain.Model;

using Main.Common.Model;

using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace Main.Services;

public interface IAccountService
{
    Task<IdentityResult> CreateIdentityUserAccount (UserAccountDataModel userAccountDataModel);

    Task<ApplicationUser?> GetIdentityUser (string email);

    Task<SignInResult> AuthenticateUser (string email,string password);

    Task<bool> ChangePasswordAsync (string email,string password,string rePassword);

    Task<bool> UnlockUser (string userId);

    Task<List<IdentityUserDataModel>?> Users ();

    Task<string?> GetEmailVerifyToken (string email);

    Task<bool> CreateApplicationUser (string email,string token,BaseDataModel baseDataModel);

    Task<ClaimsIdentity?> GetUserRole (string email,string tenantId);

}
