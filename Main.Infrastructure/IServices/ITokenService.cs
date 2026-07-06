using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Main.Infrastructure;

public interface ITokenService
{
    string GenerateAccessToken (string userId,string tenantId,
    IEnumerable<string> roles,int expiryInMinutes);

    string GenerateRefreshToken ();

    ClaimsPrincipal? ValidateAndDecryptToken (string token,out SecurityToken? validatedToken);
}