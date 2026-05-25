
using Domain.Model;
using IRepository;
using Main.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserRepository : IUserRepository
{
    private readonly BussinessAppDbContext _Context;

    public UserRepository( BussinessAppDbContext context )
    {
        _Context = context;
    }

    public async Task<int> GetAddedUserID(User user)
    {
        if (user != null)
        {
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
        }

        return ( user != null ? user.UserID : 0 );
    }

    public async Task<User> GetSingleUser(string email)
    {
        if (string.IsNullOrEmpty(email))
            return new User();
        
        return await _Context.Users.SingleAsync(a => a.Email == email);
    }

    public async Task<User> GetSingleUserByIdentityID(string identityUserId)
    {
        if (string.IsNullOrEmpty(identityUserId))
            return new User(); 

        return await _Context.Users.SingleAsync<User> 
                        (a => a.IdentityUserID == identityUserId);
    }

    public async Task<User> GetSingleUser(int userID)
    {
        var userEntity = await _Context.Users.SingleAsync<User>
                                             (a => a.UserID == userID);

        return userEntity;
    }

    public async Task<bool> DoesUserEmailExists(string email)
    {
        return await _Context.Users.AnyAsync (a => a.Email.Trim() == email.Trim());
    }    

    public async Task<bool> UpdateUser(User user)
    {
        if (user != null)
        {
            _Context.Entry<User>(user).State = EntityState.Modified;

            await _Context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> AddUser(User user)
    {
        if (user != null)
        {
            _Context.Users.Add(user);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<List<User>> GetAllUser()
    {
        var userList = await _Context.Users.ToListAsync();

        return userList;
    }

    public async Task<List<User>> GetAllUser(bool allTypesOfUser)
    {
        var userList = await _Context.Users
            .OrderBy(a=>a.IsActive)
            .ToListAsync();

        return userList;
    }
}
