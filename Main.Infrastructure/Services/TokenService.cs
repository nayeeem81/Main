using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Main.Infrastructure;

public class TokenService: ITokenService
{
    private readonly TokenValidationParameters _validationParameters;
    private readonly byte[] _signingKey;

    public TokenService (IConfiguration config)
    {
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

    public string GenerateAccessToken
    (string userId,string tenantId,IEnumerable<string> roles,int expiryInMinutes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new("tenant_id", tenantId)
        };

        claims.AddRange (roles.Select (role => new Claim (ClaimTypes.Role,role)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken (token);
    }

    public string GenerateRefreshToken () => Convert.ToBase64String (RandomNumberGenerator.GetBytes (62));

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
}