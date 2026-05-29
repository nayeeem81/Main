using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

using WebApp.ViewModel;
using WebApp.ViewModel.Extensions;
using Main.Services;
using Main.Common.HelperRelated;
using ResourceLibrary.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;


namespace Main.WebAppCore;

public class AuthController : BaseController
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserContext _userContext;
    private readonly ILogger<AuthController> _logger;
    private readonly IAccountService _userAccountService;

    public IUserContext UserContext => _userContext;

    public AuthController (
        ILogger<AuthController> logger,
        IStringLocalizer<SharedResource> localizer,
        IAccountService userAccountService,
        IUserContext userContext
       )
    {
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
            return RedirectToAction ( "Signup" );
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
        var accountDisplayViewModel = new AccountDisplayViewModel("Sign in");

        return View(accountDisplayViewModel);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login( AccountDisplayViewModel accountDisplayViewModel )
    {
        if ( ModelState.IsValid )
        {
            return RedirectToAction ( "Login" );
        }

        var result = await _userAccountService.AuthenticateUser ( accountDisplayViewModel.Email, accountDisplayViewModel.Password );


        if (result)
        {

            int userID = await _userAccountService.GetSingleUser(accountDisplayViewModel.Email);

            if (userID == 0)
            {
                return RedirectToAction ( "Login" );
            }

           
            var claims = new List<Claim> {

                new Claim ( ClaimTypes.Name, accountDisplayViewModel.Email ) ,
                new Claim ( ClaimTypes.NameIdentifier, userID.ToString()) 

            };   

            var identity = new ClaimsIdentity ( claims, IdentityConstants.ApplicationScheme  );

            var principal = new ClaimsPrincipal (identity);

            await HttpContext.SignInAsync ( IdentityConstants.ApplicationScheme, principal );

            return RedirectToAction ("Index", "Home");
        }

        return RedirectToAction ( "Login" );
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }


    [Authorize (Roles = "Admin,User,Company")]
    public IActionResult ResetPassword( )
    {
        if ( _userContext.User == null )
        {
            _logger.LogWarning ( "User context is null in ResetPassword GET action." );
            return RedirectToAction ( "Login","Auth" );
        }
                    
        var objModel = new AccountDisplayViewModel("Reset Password");

        return View(objModel);
    }



    [HttpPost]
    [Authorize(Roles = "Admin,User,Company")]
    public async Task<ActionResult> ResetPassword (
        AccountDisplayViewModel accountDisplayViewModel)  
    {
        var isValid = ValidationRelated.IsValidEmail(accountDisplayViewModel.Email);
        
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
