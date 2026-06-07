using DataTransferModel;
using IRepository; 
using Main.Common.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace Main.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountService ( 
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        )
    {
        _userManager = userManager;

        _signInManager = signInManager;
    }



    public async Task<IdentityResult> CreateIdentityUserAccount ( UserAccountDataModel userAccountDataModel )
    {
        IdentityUser userIdentityEntity = CreateIdentityUser(userAccountDataModel);

        var resultCreateIdentityUser = await 
                                        _userManager
                                        .CreateAsync(userIdentityEntity, 
                                         userAccountDataModel.Password);

        await _userManager.AddToRoleAsync ( userIdentityEntity,"User" );

        return resultCreateIdentityUser;
    }


    public async Task<IdentityUser?> GetIdentityUser ( string email )
    {
        IdentityUser? identityUser  
            = await _userManager.FindByEmailAsync ( email );

        return identityUser;
    }


    public async Task<SignInResult> AuthenticateUser ( string email, string password )
    {
        var userIdentity = await _userManager.FindByEmailAsync(email.Trim());

        if ( userIdentity == null )
        {
            return SignInResult.Failed;
        }

        var result = await _signInManager.PasswordSignInAsync ( userIdentity, password, true,   lockoutOnFailure: false);
        
        return result;
    }


    public async Task<bool> ChangePasswordAsync ( string email,string password,
        string rePassword )
    {
        IdentityUser? identityUser = await GetIdentityUser ( email );

        if ( identityUser == null )
        {
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(identityUser, password, rePassword);

        return result.Succeeded;
    }


    public async Task<string?> GetEmailVerifyToken ( string email )
    {
        IdentityUser? user = await _userManager.FindByEmailAsync ( email );
        
        if ( user == null )
        {
            return null;
        }
        
        var code = await _userManager.GenerateEmailConfirmationTokenAsync (user);

        return code;    
    }


    public async Task<bool> CreateAppicationUser ( string email, string token, BaseDataModel baseDataModel )
    {
        var userIdentity = await _userManager.FindByEmailAsync (email);

        if ( userIdentity != null)
        {
            await _userManager.ConfirmEmailAsync ( userIdentity, token );

            return true;
        }

        return false;
    }

    public async Task<ClaimsIdentity?> GetUserRole ( string email )
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);

        if (!roles.Any())
        {
            return null;
        }

        var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));

        return new ClaimsIdentity(claims);
    }



    #region Priate Methods (CreateUser, CreateIdentityUser, CreateAppicationUser, RemoveIdentityUser)

    

    private IdentityUser CreateIdentityUser ( UserAccountDataModel userAccountDataModel )
    {
        var userIdentity = new IdentityUser
        {
            Email = userAccountDataModel.Email,

            PhoneNumber = userAccountDataModel.PhoneNumber,

            NormalizedUserName = userAccountDataModel.Email.ToUpper(),

            UserName = userAccountDataModel.UserName
        };

        return userIdentity;
    }

    public async Task<bool> UnlockUser ( string userId )
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

    public async Task<List<IdentityUserDataModel>?> Users ( )
    {
        List<IdentityUser> identityUsers = await _userManager.Users.ToListAsync<IdentityUser>();

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

