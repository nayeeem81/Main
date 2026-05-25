using Microsoft.AspNetCore.Identity;

namespace Main.Services;

public interface IAccountService
{
    Task<bool> CreateUserAccount ( UserAccountDataModel userAccountDataModel );

    Task<int> GetSingleUser ( string email );

    Task<IdentityUser?> GetIdentityUser ( string email );

    Task<bool> AuthenticateUser ( string email,string password );

    Task<bool> ChangePasswordAsync ( string email,
                                string password,
                                string rePassword );
}
