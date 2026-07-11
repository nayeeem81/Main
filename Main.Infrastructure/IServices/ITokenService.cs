using Main.Common;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Main.Infrastructure;

public interface ITokenService
{
    Task<string> GenerateAccessToken (string userId,string tenantId,int expiryInMinutes);

    string GenerateRefreshToken ();

    Task<bool> SaveRefreshToken (string userId,string tenantId,string token);

    ClaimsPrincipal? ValidateAndDecryptToken (string token,out SecurityToken? validatedToken);

    Task<bool> RevokeUserRefreshTokensAsync (string userId,string tenantId);

    Task<TokenResponseModel> RotateRefreshTokenAsync (string currentToken,string tenantId,string userId);
}