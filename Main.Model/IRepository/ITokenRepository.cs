using Domain.Model;

namespace Main.IRepository;

public interface ITokenRepository
{
    Task<bool> LogoutRevokeUserRefreshTokensAsync (string userId,Guid tenantId);

    Task<UserRefreshToken?> GetSavedRefreshTokenAsync (string userId,Guid tenantId);

    Task<bool> UpdateTokenAsync (UserRefreshToken userRefreshToken);

    Task<bool> RevokeAllUserTokensAsync (string userId,Guid tenantId);

    Task<bool> SaveRotateRefreshTokenAsync (string token,string userId,Guid tenantId);

    Task<bool> SaveTokenAsync (string userId,Guid tenantId,string token);
}