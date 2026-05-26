using Main.Common.HelperRelated;
using Main.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ResourceLibrary;
using System.Security.Claims;
using WebApp.ViewModel;
using WebApp.ViewModel.Extensions;

namespace Main.WebAppCore;

public class AuthController : BaseController
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IConfiguration _configuration; 
    private readonly IUserContext _userContext;
    private readonly ILogger<AuthController> _logger;
    private readonly IAccountService _userAccountService;

    public AuthController (
        ILogger<AuthController> logger,
        IConfiguration configuration,
        IStringLocalizer<SharedResource> localizer,
        IAccountService userAccountService,
        IUserContext userContext
       )
    {
        _configuration = configuration;
        _userAccountService = userAccountService;
        _localizer = localizer;
        _logger = logger;
        _userContext = userContext;
    }

    public IActionResult Signup()
    {
        var objModel = new AccountDisplayViewModel("Registration Page");
               
        return View(objModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp( AccountDisplayViewModel accountDisplayViewModel )
    {
        if ( accountDisplayViewModel == null )
        {
            _logger.LogWarning ( "Received null AccountDisplayViewModel in SignUp POST action." );
            return RedirectToAction ( "Signup" );
        }

        if ( ModelState.IsValid )
        {
             return BadRequest ( "Invalid data submitted." );
        }

        if(!AuthExtensions.CheckPasswordMatch ( accountDisplayViewModel.Password,accountDisplayViewModel.RePassword ) )
        {
            _logger.LogWarning ( "Password and RePassword do not match for email: {Email}",accountDisplayViewModel.Email );
            return RedirectToAction ( "Signup" );
        }


        UserAccountDataModel userAccountDataModel 
            = AuthExtensions.MapToDataModel (accountDisplayViewModel);


        bool result 
            = await _userAccountService.CreateUserAccount ( userAccountDataModel );

        return RedirectToAction("Login");    
    }
    

    public IActionResult Login()
    {
        var accountDisplayViewModel = new AccountDisplayViewModel("Login");

        return View(accountDisplayViewModel);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        AccountDisplayViewModel accountDisplayViewModel )
    {
        var result = await _userAccountService.AuthenticateUser ( accountDisplayViewModel.Email, accountDisplayViewModel.Password );

        if (result)
        {
            int userID = await _userAccountService.GetSingleUser(accountDisplayViewModel.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, 
                          userID.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync ( CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal ( claimsIdentity ) );

            return RedirectToAction("Index", "Home");
        }

        return BadRequest("Invalid credentials");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }


    [Authorize(Roles = "Admin,User,Company")]
    public ActionResult ResetPassword()
    {
        if ( ! HttpContext.User.Identity.IsAuthenticated )
        {
            var objModel = new AccountDisplayViewModel("Reset Password");

            return View(objModel);
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User,Company")]
    public async Task<ActionResult> ResetPassword (
        AccountDisplayViewModel accountDisplayViewModel)  
    {
        var isValid = ValidationRelated
            .IsValidEmail(accountDisplayViewModel.Email);
        
        _logger.LogInformation("Password reset attempt for email: {Email}, Valid Email: {IsValid}", accountDisplayViewModel.Email, isValid);
        
        if(!isValid)
        {
            _logger.LogWarning("Invalid email format for password reset: {Email}", accountDisplayViewModel.Email);

            return RedirectToAction("ResetPassword");
        }

        try
        {
            var result = await _userAccountService.ChangePasswordAsync ( 
                accountDisplayViewModel.Email,
                accountDisplayViewModel.Password,
                accountDisplayViewModel.RePassword );

            if ( result == false )
            {
                _logger.LogWarning ( "Password reset attempt failed for email: {Email}",accountDisplayViewModel.Email );

                return RedirectToAction ( "ResetPassword" );
            }

            if (result)
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("ResetPassword");
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            _logger.LogError(ex, "Error sending password reset email to {Email}", accountDisplayViewModel.Email);
            return RedirectToAction("ResetPassword");
        }
    }

    //[HttpPost]
    //public async Task<JsonResult> EmailVerify()
    //{
    //    try
    //    {
    //        var codeValue = "";
    //        UserModel userModel = GetSessionUser();
    //        if (!userModel.IsVerifiedUser.Value && userModel != null)
    //        {
    //            codeValue = await _userAccountService.UpdateVerifyCode(userModel.UserID);
    //            var objEmailViewModel = _EmailService.GetVerifyEmailViewModel(codeValue);
    //            objEmailViewModel.MessageBodyHTMLText = await FindMyView(this, "_VerifyEmail", objEmailViewModel);
    //            objEmailViewModel.ReceiverEmail = userModel.Email;
    //            _EmailService.SendAccountVerifyEmail(objEmailViewModel);
    //            return Json(false);
    //        }
    //        return Json(true);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error sending account verification email to user ID: {UserID}", GetSessionUser()?.UserID);
    //        var msg = ex.Message;
    //        return Json(false);
    //    }
    //}
}
