using DataTransferModel;

using Domain.Model;

using Main.Common.Model;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
namespace Main.Services;

public class AccountService: IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService (
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        )
    {
        _userManager = userManager;

        _signInManager = signInManager;
    }



    public async Task<IdentityResult> CreateIdentityUserAccount (UserAccountDataModel userAccountDataModel)
    {
        ApplicationUser userIdentityEntity = CreateIdentityUser(userAccountDataModel);

        var resultCreateIdentityUser = await
        _userManager
        .CreateAsync(userIdentityEntity,
        userAccountDataModel.Password);

        _ = await _userManager.AddToRoleAsync (userIdentityEntity,"User");

        return resultCreateIdentityUser;
    }


    public async Task<ApplicationUser?> GetIdentityUser (string email)
    {
        ApplicationUser? identityUser
            = await _userManager.FindByEmailAsync ( email );

        return identityUser;
    }


    public async Task<SignInResult> AuthenticateUser (string email,string password)
    {
        var userIdentity = await _userManager.FindByEmailAsync(email.Trim());

        if ( userIdentity == null )
        {
            return SignInResult.Failed;
        }

        var result = await _signInManager.PasswordSignInAsync ( userIdentity, password, true,   lockoutOnFailure: false);

        return result;
    }


    public async Task<bool> ChangePasswordAsync (string email,string password,
        string rePassword)
    {
        ApplicationUser? identityUser = await GetIdentityUser ( email );

        if ( identityUser == null )
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(identityUser, password, rePassword);

        return result.Succeeded;
    }


    public async Task<string?> GetEmailVerifyToken (string email)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync ( email );

        if ( user == null )
        {
            return null;
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync (user);

        return code;
    }


    public async Task<bool> CreateApplicationUser (string email,string token,BaseDataModel baseDataModel)
    {
        var userIdentity = await _userManager.FindByEmailAsync (email);

        if ( userIdentity != null )
        {
            _ = await _userManager.ConfirmEmailAsync (userIdentity,token);

            return true;
        }

        return false;
    }

    public async Task<ClaimsIdentity?> GetUserRole (string email,string tenantId)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
        if ( user == null )
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);
        if ( !roles.Any () )
        {
            return null;
        }

        if ( roles[0] == "GlobalAdmin" )
        {
            List<Claim>  claims = [new Claim (ClaimTypes.Role,roles[0])];
            return new ClaimsIdentity (claims);
        }

        var userTenants = user.UserTenants.ToList<UserTenant>();
        if ( userTenants != null && userTenants.Any () )
        {
            UserTenant? currentUserTenant = userTenants.FirstOrDefault<UserTenant>
            (a => a.TenantId == tenantId);

            if ( currentUserTenant != null )
            {
                string? tenantRole =  currentUserTenant.TenantRole;
                var roleClaim = $"{tenantId}:{tenantRole}";

                Claim claimRole = new("TenantRole", roleClaim);
                ( ( List<Claim> ) [] ).Add (claimRole);
                return new ClaimsIdentity (( List<Claim> ) []);
            }
        }
        return null;
    }



    #region Priate Methods (CreateUser, CreateIdentityUser, CreateAppicationUser, RemoveIdentityUser)



    private ApplicationUser CreateIdentityUser (UserAccountDataModel userAccountDataModel)
    {
        var userIdentity = new ApplicationUser
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
        var user = await _userManager.FindByIdAsync(userId);

        if ( user == null )
        {
            return false;
        }

        var lockoutResult = await _userManager.SetLockoutEndDateAsync(user, null);

        if ( !lockoutResult.Succeeded )
        {
            return false;
        }

        var resetResult = await _userManager.ResetAccessFailedCountAsync(user);

        if ( !resetResult.Succeeded )
        {
            return false;
        }

        return true;
    }

    public async Task<List<IdentityUserDataModel>?> Users ()
    {
        List<ApplicationUser> identityUsers = await _userManager.Users.ToListAsync<ApplicationUser>();

        List<IdentityUserDataModel> identityUserDataModel = identityUsers.Select(u => new IdentityUserDataModel
        {
            UserId = u.Id,
            UserName = u.UserName,
            LockoutEnd = u.LockoutEnd
        }).ToList<IdentityUserDataModel>();

        return identityUserDataModel;
    }





    #endregion
}

