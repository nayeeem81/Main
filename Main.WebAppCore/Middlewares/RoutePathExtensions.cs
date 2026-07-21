
namespace Main.WebAppCore.Middleware;

public static class ReutePathExtensions
{
    public static string ResolveFromPath (this HttpContext context,string? tenantPath)
    {
        string? pathRequest;

        if ( tenantPath == null )
        {
            pathRequest = context.Request.Path.Value ?? "";
        }
        else
        {
            pathRequest = tenantPath;
        }

        var pathSegments = pathRequest?.Split('/', StringSplitOptions.RemoveEmptyEntries);

        string? tenantName =
            pathSegments?.Length > 0 ? pathSegments[0] : string.Empty;

        RewitePathForSubdirectoryHost (context,tenantName);

        return tenantName;
    }

    private static void RewitePathForSubdirectoryHost (HttpContext context,
    string? tenantName)
    {
        if ( tenantName != string.Empty )
        {
            context.Items["TenantSubDirectoryName"] = tenantName;
            context.Request.PathBase = new PathString ($"/{tenantName}");
            string path = context.Request.Path;
            context.Request.Path = path.Substring (tenantName!.Length + 1);
        }
    }

}