using DataTransferModel;

using Domain.Model;
using Main.Common;
using Main.IRepository;
using Microsoft.AspNetCore.Identity;
namespace Main.Services;

public class AccountService: IAccountService
{
    private readonly IApplicationUserRepository _userRepository;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantUserRepository _tenantUserRepository;

    public AccountService (IApplicationUserRepository userRepository,ITenantRepository tenantRepository,ITenantUserRepository tenantUserRepository)
    {
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
        _tenantUserRepository = tenantUserRepository;
    }

    public async Task<IdentityResult> CreateApplicationUserAccount (UserAccountDataModel userAccountDataModel)
    {
        ApplicationUser userIdentityEntity = CreateApplicationUser(userAccountDataModel);

        bool resultCreateIdentityUser = await
        _userRepository.CreateAsync(userIdentityEntity, userAccountDataModel.Password);

        if ( resultCreateIdentityUser )
        {
            _ = await _userRepository.AddToRoleAsync (userIdentityEntity.Email!,"User");

            Tenant? tenant = new ()
            {
                Name = userAccountDataModel.ClientName,
                TenantHostType = HostType.SubDomain,
                HostName = userAccountDataModel.ClientName.Replace(" ", "-").ToLower(),
                Store = StoreType.Defaut
            };


            tenant = await _tenantRepository.CreateTenantAsync (tenant);

            if ( tenant == null )
            {
                IdentityError[] errors = [];
                IdentityResult result = IdentityResult
                    .Failed( errors );
                return result;
            }


            TenantUser tenantUser = new ()
            {
                TenantId = tenant?.TenantId!,
                UserId =  userIdentityEntity.Id ,
                TenantRole = "Admin"
            };

            await _tenantUserRepository.AddAsync (tenantUser);

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


    public async Task<ApplicationUserDataModel?> GetApplicationUser
    (string email,string tenantId)
    {
        ApplicationUser? applicationUser
        = await _userRepository.FindByEmailAsync ( email );

        if ( applicationUser == null )
        {
            return null;
        }

        TenantUser? tenantUser =
        await _tenantUserRepository.GetByUserIdAsync(applicationUser?.Id!, tenantId);

        if ( tenantUser == null )
        {
            return null;
        }

        ApplicationUserDataModel? applicationUserDataModel
        = new ()
        {
            Id = applicationUser?.Id!,
            UserName = applicationUser?.UserName,
            Email = applicationUser?.Email,
            TenantId= tenantUser.TenantId
        };

        return applicationUserDataModel;
    }

    public async Task<SignInResult> AuthenticateUser (string email,string password)
    {
        bool result = await _userRepository.PasswordSignInAsync (email, password, true, false);

        return result ? SignInResult.Success : SignInResult.Failed;
    }

    public async Task<bool> ChangePasswordAsync (string email,string password,string rePassword)
    {
        bool result = await _userRepository.ChangePasswordAsync(email, password, rePassword);

        return result;
    }

    public async Task<string?> GetEmailVerifyToken (string email)
    {
        string? code = await _userRepository.GenerateEmailConfirmationTokenAsync (email);
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

        ApplicationUserDataModel? applicationUserDataModel = new ()
        {
            Id = applicationUser.Id,
            UserName = applicationUser.UserName,
            Email = applicationUser.Email
        };

        return applicationUserDataModel;
    }


    public async Task<bool> CreateApplicationUser (string email,string token)
    {
        var userIdentity = await _userRepository.FindByEmailAsync (email);

        if ( userIdentity != null )
        {
            bool result = await _userRepository.ConfirmEmailAsync (email,token);

            return result;
        }

        return false;
    }

    public async Task<string> GetTenantUserRoleClaim (string email,string tenantId)
    {
        var identityUser = await _userRepository.FindByEmailAsync(email);
        string userId = identityUser!.Id;

        string? tenantRole = await _userRepository.GetTenantRolesAsync (email, tenantId);

        if ( tenantRole != null )
        {
            tenantRole = "";
        }

        var expectedClaimValue = $"{userId}:{tenantId}:{tenantRole!.ToString()}";

        return expectedClaimValue;
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

        var result = await _userRepository.SetLockoutEndDateAsync(user!.Email!);

        if ( result )
        {
            return true;
        }

        bool resetResult = await _userRepository.ResetAccessFailedCountAsync(user!.Email!);

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

