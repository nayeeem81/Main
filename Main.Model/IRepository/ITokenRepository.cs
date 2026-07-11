using Domain.Model;

namespace Main.IRepository;

public interface ITokenRepository
{
    Task<bool> LogoutRevokeUserRefreshTokensAsync (string userId,string tenantId);

    Task<UserRefreshToken?> GetSavedRefreshTokenAsync (string userId,string tenantId);

    Task<bool> UpdateTokenAsync (UserRefreshToken userRefreshToken);

    Task<bool> RevokeAllUserTokensAsync (string userId,string tenantId);

    Task<bool> SaveRotateRefreshTokenAsync (string token,string userId,string tenantId);

    Task<bool> SaveTokenAsync (string userId,string tenantId,string token);
}