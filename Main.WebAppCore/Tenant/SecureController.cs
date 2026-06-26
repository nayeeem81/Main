using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineArtsWebApp
{
    [Authorize]
    public class SecureController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "This is a secure endpoint" });
        }

        [Authorize(Policy = "RequireAdminRole")]
        public IActionResult AdminOnly()
        {
            return Ok(new { message = "This is an admin-only endpoint" });
        }
    }
}