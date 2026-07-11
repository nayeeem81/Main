using Domain.Model;
using Main.Common;
using Main.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Main.Infrastructure;

public class TokenService: ITokenService
{
    private readonly TokenValidationParameters _validationParameters;
    private readonly byte[] _signingKey;
    private readonly ITokenRepository _tokenRepository;

    public TokenService (IConfiguration config,ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
        _signingKey = Encoding.UTF8.GetBytes (config["Jwt:Secret"]!);
        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey (_signingKey),
            ValidateIssuer = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = config["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }

    public async Task<string> GenerateAccessToken
    (string userId,string tenantId,int expiryInMinutes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new("tenant_id", tenantId)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        _ = await SaveRefreshToken (userId,tenantId,token.ToString () ?? "");

        return tokenHandler.WriteToken (token);
    }

    public string GenerateRefreshToken () =>
        Convert.ToBase64String (RandomNumberGenerator.GetBytes (62));



    public async Task<bool> RevokeUserRefreshTokensAsync (string userId,string tenantId)
    {
        bool result = await _tokenRepository.LogoutRevokeUserRefreshTokensAsync(userId,tenantId);
        return result;
    }

    public async Task<bool> SaveRefreshToken (string userId,string tenantId,string token)
    {
        bool result = await _tokenRepository.SaveTokenAsync(userId,tenantId,token);
        return result;
    }

    public ClaimsPrincipal? ValidateAndDecryptToken (string token,out SecurityToken? validatedToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            return tokenHandler.ValidateToken (token,_validationParameters,out validatedToken);
        }
        catch
        {
            validatedToken = null;
            return null;
            // Return null if validation fails
        }
    }

    public async Task<TokenResponseModel> RotateRefreshTokenAsync (string currentToken,string tenantId,string userId)
    {
        UserRefreshToken? savedToken = await _tokenRepository.GetSavedRefreshTokenAsync(currentToken,tenantId);

        if ( savedToken == null )
        {
            return new TokenResponseModel (false);
        }

        if ( savedToken.IsRevoked )
        {
            _ = await _tokenRepository.RevokeAllUserTokensAsync (userId,tenantId);
            throw new SecurityException ("Refresh token reuse detected! Compromise suspected. All sessions revoked.");
        }

        if ( savedToken.ExpiresAt <= DateTime.UtcNow )
        {
            return new TokenResponseModel (false);
        }

        var newAccessJwtStr = GenerateRefreshToken ();

        if ( savedToken != null )
        {
            savedToken.IsRevoked = true;
            savedToken.ReplacedByToken = newAccessJwtStr.ToString ();
            _ = await _tokenRepository.UpdateTokenAsync (savedToken);
        }

        bool result = await _tokenRepository.SaveRotateRefreshTokenAsync(newAccessJwtStr.ToString()?? "", userId,tenantId);

        var newAccessJwt = await GenerateAccessToken(userId, tenantId, 20);

        TokenResponseModel tokenResponseModel = new(result)
        {
            AccessToken =  newAccessJwt,
            RefreshToken =  newAccessJwtStr.ToString()?? ""
        };

        return tokenResponseModel;
    }
}