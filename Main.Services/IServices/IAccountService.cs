namespace Main.Services;

public interface IAccountService
{
    Task<bool> CreateUserAccount (
        UserAccountDataModel userAccountDataModel );

    Task<int> GetSingleUser ( string id );
}
