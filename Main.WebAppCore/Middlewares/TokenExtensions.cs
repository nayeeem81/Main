namespace Main.WebAppCore.Middlewares;

public static class TenantSafetyCheckExtensions
{
    public static bool CheckContamination (string resolvedTenantId,HttpContext context)
    {
        if ( context.User?.FindFirst ("tenant_id") == null )
        {
            return true;
        }

        var tenantId = context.User?.FindFirst ("tenant_id")?.Value ?? "";

        if ( resolvedTenantId == tenantId )
        {
            return true;
        }

        return false;
    }
}
