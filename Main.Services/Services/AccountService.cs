using DataTransferModel;

using Domain.Model;

using FluentEmail.Core;

using IRepository;

using Main.Common.HelperRelated;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Main.Services;

public class AccountService : IAccountService
{

    private readonly IUserRepository _userRepository;

    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountService ( 
        IUserRepository userRepository,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        )
    {
        _userRepository = userRepository;

        _userManager = userManager;

        _signInManager = signInManager;
    }



    public async Task<bool> CreateIdentityUserAccount ( UserAccountDataModel userAccountDataModel )
    {
        IdentityUser userIdentityEntity = CreateIdentityUser(userAccountDataModel);

        var resultCreateIdentityUser = await 
                                        _userManager
                                        .CreateAsync(userIdentityEntity, 
                                         userAccountDataModel.Password);

        if ( resultCreateIdentityUser.Succeeded )
        {
            return true;
        }

        return false;
    }



    public async Task<int> GetSingleUser ( string email )
    {
        User? userEntity  = await _userRepository.GetSingleUser ( email.Trim() );

        return userEntity?.UserID ?? 0;
    }



    public async Task<IdentityUser?> GetIdentityUser ( string email )
    {
        IdentityUser? identityUser  
            = await _userManager.FindByEmailAsync ( email );

        return identityUser;
    }



    public async Task<bool> AuthenticateUser ( string email, string password )
    {
        var userIdentity = await _userManager.FindByEmailAsync(email.Trim());

        if ( userIdentity == null )
        {
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync ( userIdentity, password, true,   lockoutOnFailure: false);
        
        return result.Succeeded;
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



    public async Task<bool> CreateAppicationUser ( string email, string token )
    {
        var userIdentity = await _userManager.FindByEmailAsync (email);

        var success = await CreateUser( userIdentity );

       
        if ( success  && userIdentity != null)
        {
            await _userManager.ConfirmEmailAsync ( userIdentity, token );

            await _userManager.AddToRoleAsync ( userIdentity, "User" );

            return true;
        }

        return false;
    }



    #region Priate Methods (CreateUser, CreateIdentityUser, CreateAppicationUser, RemoveIdentityUser)

    private async Task<bool> CreateUser ( IdentityUser? userIdentity )
    {
        UserAccountDataModel userAccountDataModel = new UserAccountDataModel
        {
            Email = userIdentity.Email,
            PhoneNumber = userIdentity.PhoneNumber,
            UserName = userIdentity.UserName
        };

        User userEntity = CreateUserEntity ( userIdentity.Id, userAccountDataModel );

        bool success = await _userRepository.AddUser ( userEntity );

        return success;
    }

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

    private User CreateUserEntity ( string idetytyId, 
                                    UserAccountDataModel userAccountDataModel )
    {
        User objUserEntity = new User();

        objUserEntity.IdentityUserID = idetytyId;

        objUserEntity.Email = userAccountDataModel.Email;

        objUserEntity.ClientName = 
            StringRelated.GetUserNameFromEmail ( userAccountDataModel.Email );

        objUserEntity.CreateBaseData ( userAccountDataModel.BaseDataModel );

        return objUserEntity;
    }

    

    private async Task RemoveIdentityUser
            ( bool success,string email )
    {
        if ( success )
        {
            var user = await _userManager
                       .FindByIdAsync (email);

            if ( user != null )
            {
                var resultDelete 
                    = await 
                    _userManager
                    .DeleteAsync(user);
            }
        }
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

