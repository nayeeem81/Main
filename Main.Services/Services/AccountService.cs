using DataTransferModel;

using Domain.Model;

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
        IUserContext userContext, 
        IUserRepository userRepository,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        )
    {
        _userRepository = userRepository;

        _userManager = userManager;

        _signInManager = signInManager;
    }

    public async Task<bool> CreateUserAccount ( UserAccountDataModel userAccountDataModel )
    {
        IdentityUser userIdentityEntity = CreateIdentityUser(userAccountDataModel);

        var resultCreateIdentityUser = await 
                                        _userManager
                                        .CreateAsync(userIdentityEntity, 
                                         userAccountDataModel.Password);

        if ( resultCreateIdentityUser.Succeeded )
        {
            bool result = await CreateAppicationUser ( userAccountDataModel );

            return result;
        }

        return false;
    }

    public async Task<int> GetSingleUser ( string email )
    {
        User userEntity  = await _userRepository.GetSingleUser ( email );

        return userEntity.UserID;
    }

    public async Task<IdentityUser?> GetIdentityUser ( string email )
    {
        IdentityUser? identityUser  
            = await _userManager.FindByEmailAsync ( email );

        return identityUser;
    }

    public async Task<bool> AuthenticateUser ( string email, string password )
    {
        var userIdentity =
            await _userManager
                  .FindByEmailAsync(email);

        if ( userIdentity == null )
        {
            return false;
        }

        var result = await
                     _signInManager.PasswordSignInAsync
                     (userIdentity, password, true, lockoutOnFailure: false);
        
        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync (
        string email,string password,string rePassword )
    {
        IdentityUser? identityUser = await GetIdentityUser ( email );

        if ( identityUser == null )
        {
            return false;
        }

        var result = await _userManager
            .ChangePasswordAsync(identityUser, password, rePassword);

        return result.Succeeded;
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

    private async Task<bool> CreateAppicationUser
        ( UserAccountDataModel userAccountDataModel )
    {
        var userSame =
                await _userManager.FindByIdAsync (userAccountDataModel.Email);

        if ( userSame != null )
        {
            User userEntity =
                    CreateUserEntity
                    ( userSame.Id, userAccountDataModel );

            bool success =
                await _userRepository
                      .AddUser ( userEntity );
            
            
            await RemoveIdentityUser
                ( !success,userAccountDataModel.Email );

            if ( success )
            {
                await _userManager.AddToRoleAsync ( userSame,"User" );
            }
            
            return success;
        }

        return false;
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

