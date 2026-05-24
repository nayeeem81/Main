using Microsoft.AspNetCore.Identity;
using Domain.Model;
using IRepository;
using Main.Common.HelperRelated;

namespace Main.Services;

public class AccountService : IAccountService
{
    private readonly IUserContext _userContext;
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
        _userContext = userContext;
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

    public async Task<int> GetSingleUser ( string id )
    {
        User userEntity  = await _userRepository.GetSingleUserByIdentityID ( id );

        return userEntity.UserID;
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
            
            // if Not Success (Reverce)
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

#endregion

    
}
