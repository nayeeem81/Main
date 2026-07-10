using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore.Controllers.AuthService;

public static class UrlExtensions
{
    public static string GenerateUrlLink
    (this IUrlHelper urlHelper,
    string email,
    string token,
    string action,
    string controller,
    HttpContext context)
    {
        string? url =
        urlHelper.Action
        ( action, controller,  new
        {
            Email = email,Token = token
        }, context.Request.Scheme
        );

        return string.IsNullOrEmpty (url) ? "" : url;
    }
}