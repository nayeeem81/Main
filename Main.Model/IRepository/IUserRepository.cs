using Domain.Model;

namespace IRepository;

public interface IUserRepository
{
    Task<bool> AddUser(User user);

    Task<bool> UpdateUser(User user);

    Task<User?> GetSingleUser(string email);

    Task<User?> GetSingleUser(int userId);

    Task<User?> GetSingleUserByIdentityID(string identityUserId);

    Task<bool> DoesUserEmailExists(string email);

    Task<int> GetAddedUserID(User user);

    Task<List<User>> GetAllUser();

    Task<List<User>> GetAllUser(bool allTypesOfUser);

}