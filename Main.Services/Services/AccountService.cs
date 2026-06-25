using DataTransferModel;

using Domain.Model;

using Main.Common;
using Main.IRepository;
using Microsoft.AspNetCore.Identity;

using System.Security.Claims;
namespace Main.Services;

public class AccountService: IAccountService
{
    private readonly IApplicationUserRepository _userRepository;

    public AccountService (IApplicationUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IdentityResult> CreateApplicationUserAccount (UserAccountDataModel userAccountDataModel)
    {
        ApplicationUser userIdentityEntity = CreateApplicationUser(userAccountDataModel);

        bool resultCreateIdentityUser = await
        _userRepository.CreateAsync(userIdentityEntity, userAccountDataModel.Password);

        if ( resultCreateIdentityUser )
        {
            await _userRepository.AddToRoleAsync (userIdentityEntity,"User");
            return IdentityResult.Success;
        }
        else
        {
            IdentityError[] errors = [];
            IdentityResult result = IdentityResult
                .Failed( errors );
            return result;
        }
    }


    public async Task<ApplicationUserDataModel?> GetApplicationUser (string email)
    {
        ApplicationUser? applicationUser
            = await _userRepository.FindByEmailAsync ( email );

        if ( applicationUser == null )
        {
            return null;
        }

        ApplicationUserDataModel? applicationUserDataModel = new
()
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email
        };
        return applicationUserDataModel;
    }

    public async Task<SignInResult> AuthenticateUser (string email,string password)
    {
        var applicationUser = await _userRepository.FindByEmailAsync(email.Trim());

        if ( applicationUser == null )
        {
            return SignInResult.Failed;
        }

        bool result = await _userRepository.PasswordSignInAsync (applicationUser, password, true, false);

        return result ? SignInResult.Success : SignInResult.Failed;
    }

    public async Task<bool> ChangePasswordAsync (string email,string password,
        string rePassword)
    {
        ApplicationUserDataModel? applicationUser = await GetApplicationUser ( email );

        if ( applicationUser == null )
        {
            return false;
        }

        bool result = await _userRepository.ChangePasswordAsync(email, password, rePassword);

        return result;
    }

    public async Task<string?> GetEmailVerifyToken (string email)
    {
        ApplicationUser? user = await _userRepository.FindByEmailAsync ( email );

        if ( user == null )
        {
            return null;
        }

        string? code = await _userRepository.GenerateEmailConfirmationTokenAsync (user);

        return code;
    }

    public async Task<ApplicationUserDataModel?> FindByEmailAsync (string email)
    {
        ApplicationUser?  applicationUser
            = await _userRepository.FindByEmailAsync ( email );

        if ( applicationUser == null )
        {
            return null;
        }

        ApplicationUserDataModel? applicationUserDataModel = new
()
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email
        };
        return applicationUserDataModel;
    }


    public async Task<bool> CreateApplicationUser (string email,string token,BaseDataModel baseDataModel)
    {
        var userIdentity = await _userRepository.FindByEmailAsync (email);

        if ( userIdentity != null )
        {
            bool result = await _userRepository.ConfirmEmailAsync (userIdentity,token);

            return result;
        }

        return false;
    }

    public async Task GetUserClaims (string email,string tenantId)
    {
        ApplicationUser? user =
            await _userRepository.FindByEmailAsync(email);

        if ( user == null )
        {
            return;
        }

        List<string>? listRoles = await _userRepository.GetRolesAsync (email,tenantId);

        if ( listRoles == null )
        {
            return;
        }

        if ( listRoles[0] == "GlobalAdmin" )
        {
            Claim claim = new(ClaimTypes.Role,"GlobalAdmin");
            await _userRepository.AddClaimAsync (user,claim);
        }
        else
        {
            Claim claim1 = new(ClaimTypes.Role,"User");
            await _userRepository.AddClaimAsync (user,claim1);

            var expectedClaimValue = $"{tenantId}:{listRoles[0]}";

            Claim claim2 = new("TenantRole",expectedClaimValue);
            await _userRepository.AddClaimAsync (user,claim2);
        }

        await _userRepository.AddClaimAsync (user,new (ClaimTypes.Email,user.Email!));

        await _userRepository.AddClaimAsync (user,new (ClaimTypes.Name,user.UserName!));

        await _userRepository.AddClaimAsync (user,new (ClaimTypes.NameIdentifier,user.Id.ToString ()));

        await _userRepository.AddClaimAsync (user,new ("TenantId",tenantId));

    }

    private ApplicationUser CreateApplicationUser (UserAccountDataModel userAccountDataModel)
    {
        ApplicationUser userIdentity = new ()
        {
            Email = userAccountDataModel.Email,
            PhoneNumber = userAccountDataModel.PhoneNumber,
            NormalizedUserName = userAccountDataModel.Email.ToUpper(),
            UserName = userAccountDataModel.UserName
        };

        return userIdentity;
    }

    public async Task<bool> UnlockUser (string userId)
    {
        var user = await _userRepository.FindByNameIdAsync(userId);

        if ( user == null )
        {
            return false;
        }

        var result = await _userRepository.SetLockoutEndDateAsync(user);

        if ( result )
        {
            return true;
        }

        bool resetResult = await _userRepository.ResetAccessFailedCountAsync(user);

        return resetResult;
    }

    public async Task<List<ApplicationUserDataModel>?> Users ()
    {
        List<ApplicationUser>? identityUsers =
            await _userRepository.ApplicationUsers ();

        List<ApplicationUserDataModel>? identityUserDataModel = identityUsers?.Select(u => new ApplicationUserDataModel
        {
            Id = u.Id,
            UserName = u.UserName,
            LockoutEnd = u.LockoutEnd
        }).ToList<ApplicationUserDataModel>();

        return identityUserDataModel;
    }

    public async Task<bool> IsEmailConfirmedAsync (string email)
    {

        var result = await _userRepository.IsEmailConfirmedAsync (email);

        return result;
    }

    public async Task<bool> PasswordSignInAsync
    (string userName,string password,bool isPersistent,bool lockoutOnFailure)
    {
        var result = await _userRepository.PasswordSignInAsync (userName,password,isPersistent,lockoutOnFailure);

        return result;
    }

    public async Task SignOutAsync ()
    {
        await _userRepository.SignOutAsync ();
    }

    public async Task<string> GeneratePasswordResetTokenAsync (string email)
    {
        string token = await _userRepository.GeneratePasswordResetTokenAsync(email);
        return token;
    }

    public async Task<bool> ResetPasswordAsync (string email,string token,string confirmPassword)
    {
        bool result = await _userRepository.ResetPasswordAsync (email, token, confirmPassword);

        return result;
    }
}

