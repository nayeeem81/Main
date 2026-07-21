using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TokenRepository: ITokenRepository
{
    private readonly ApplicationDbContext _context;

    public TokenRepository (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> LogoutRevokeUserRefreshTokensAsync (string userId,Guid tenantId)
    {
        var activeTokens = await _context.UserRefreshTokens.Where
        (t => t.UserId == userId && t.MyTenantId == tenantId
        && !t.IsRevoked).ToListAsync();

        foreach ( var token in activeTokens )
        {
            token.IsRevoked = true;
            _ = _context.UserRefreshTokens.Update (token);
        }

        int result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<UserRefreshToken?> GetSavedRefreshTokenAsync (string userId,Guid tenantId)
    {
        UserRefreshToken? userRefreshToken =
        await _context.UserRefreshTokens.FirstOrDefaultAsync<UserRefreshToken>
        (a => a.UserId == userId && a.MyTenantId == tenantId);

        return userRefreshToken;
    }

    public async Task<bool> UpdateTokenAsync (UserRefreshToken userRefreshToken)
    {
        _ = _context.UserRefreshTokens.Update (userRefreshToken);
        var result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> SaveRotateRefreshTokenAsync (string token,string userId,Guid tenantId)
    {
        UserRefreshToken newRefreshToken = new ()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MyTenantId = tenantId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7), // Rolling expiration
            IsRevoked = false
        };

        _ = _context.UserRefreshTokens.Add (newRefreshToken);
        int  result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> RevokeAllUserTokensAsync (string userId,Guid tenantId)
    {
        var allUserTokens = await _context.UserRefreshTokens
        .Where(t => t.UserId == userId && t.MyTenantId == tenantId && !t.IsRevoked)
        .ToListAsync();

        foreach ( var token in allUserTokens )
        {
            token.IsRevoked = true;
        }

        int result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> SaveTokenAsync (string userId,Guid tenantId,string token)
    {
        UserRefreshToken newRefreshToken = new ()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MyTenantId = tenantId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7), // Rolling expiration
            IsRevoked = false
        };

        _ = _context.UserRefreshTokens.Add (newRefreshToken);
        int  result = await _context.SaveChangesAsync ();

        return result > 0;
    }

}
