using System.Security.Cryptography;
using System.Text;


namespace Main.WebAppCore.Middleware;

public class TenantExpiringTokenEngine
{
    private const long WindowSeconds = 30 * 60; // 30 minutes

    public string GenerateToken (string key,string tenantId,string tenantSecretKey)
    {
        long currentWindow = GetCurrentWindowIndex();

        return CreateHash (key,tenantId,currentWindow,tenantSecretKey);
    }

    public bool ValidateToken
    (string key,string tenantId,string suppliedToken,string tenantSecretKey)
    {
        long currentWindow = GetCurrentWindowIndex();

        // 1. Check current 30-minute block

        string currentExpected = CreateHash(key, tenantId, currentWindow, tenantSecretKey);

        if ( CryptographicOperations.FixedTimeEquals
        (Encoding.UTF8.GetBytes (currentExpected),Encoding.UTF8.GetBytes (suppliedToken)) )
        {
            return true;
        }

        // 2. Check previous 30-minute block (grace period)

        string previousExpected = CreateHash(key, tenantId, currentWindow - 1, tenantSecretKey);

        if ( CryptographicOperations.FixedTimeEquals
        (Encoding.UTF8.GetBytes (previousExpected),Encoding.UTF8.GetBytes (suppliedToken)) )
        {
            return true;
        }

        return false;
    }

    private string CreateHash (string key,string tenantId,
    long windowIndex,string tenantSecretKey)
    {
        string combinedInput = $"{tenantId.ToLowerInvariant()}:{key.ToLowerInvariant()}:{windowIndex}";

        byte[] inputBytes = Encoding.UTF8.GetBytes(combinedInput);

        byte[] secretBytes = Encoding.UTF8.GetBytes(tenantSecretKey);

        using var hmac = new HMACSHA256(secretBytes);

        byte[] hashBytes = hmac.ComputeHash(inputBytes);

        return Convert.ToHexString (hashBytes).ToLowerInvariant ();
    }

    private long GetCurrentWindowIndex ()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds () / WindowSeconds;
    }
}
