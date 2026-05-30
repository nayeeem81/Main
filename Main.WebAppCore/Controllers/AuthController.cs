using DataTransferModel;
using Main.Common.HelperRelated;
using Main.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ResourceLibrary.Resources;
using System.Security.Claims;
using System.Web;

using WebApp.ViewModel;
using WebApp.ViewModel.Extensions;

namespace Main.WebAppCore;

public class AuthController : BaseController
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserContext _userContext;
    private readonly ILogger<AuthController> _logger;
    private readonly IAccountService _userAccountService;
    private readonly IEmailSenderService _emailService;

    public AuthController (
        ILogger<AuthController> logger,
        IStringLocalizer<SharedResource> localizer,
        IAccountService userAccountService,
        IEmailSenderService emailService,
        IUserContext userContext
       )
    {
        _userAccountService = userAccountService;
        _localizer = localizer;
        _logger = logger;
        _userContext = userContext;
        _emailService = emailService;
    }



    public IActionResult Signup()
    {
        var objModel = new AccountDisplayViewModel("Registration Page");
               
        return View(objModel);
    }


    public async Task<IActionResult> VerifyEmailAsync ( string email, string token )
    {
        _logger.LogWarning ( "Verifying email for address: {Email} with token: {Token}", email, token );

        if ( string.IsNullOrEmpty ( email ) || string.IsNullOrEmpty ( token ) )
        {
            return BadRequest ( "Invalid verification request parameters." );
        }

        _logger.LogWarning ( "Attempting to create application user for email: {Email} with token: {Token}", email, token );

        var result = await _userAccountService.CreateAppicationUser ( email, token );

        _logger.LogWarning ( "Application user creation result for email: {Email} with token: {Token} is {Result}", email, token, result );

        return RedirectToAction ( "Login" );
    }



    public IActionResult VerifyEmail ( VerifyEmailViewModel verifyEmailViewModel )
    {
        return View ( verifyEmailViewModel );
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp ( AccountDisplayViewModel accountDisplayViewModel )
    {
        
        if ( accountDisplayViewModel == null && !ModelState.IsValid )
        {
            _logger.LogWarning ( "AccountDisplayViewModel is null or invalid for email: {Email}", accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );
            
            return RedirectToAction ( "Signup" );
        }


        if(!AuthExtensions.CheckPasswordMatch ( accountDisplayViewModel != null ? accountDisplayViewModel.Password : string.Empty, accountDisplayViewModel != null ? accountDisplayViewModel.RePassword : string.Empty ) )
        {
            _logger.LogWarning ( "Password and RePassword do not match for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

            return RedirectToAction ( "Signup" );
        }


        UserAccountDataModel userAccountDataModel 
            = AuthExtensions.MapToDataModel (accountDisplayViewModel != null ? accountDisplayViewModel : new AccountDisplayViewModel());

        _logger.LogWarning ( "Mapping (Data Model) completed for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

        bool result 
            = await _userAccountService.CreateIdentityUserAccount ( userAccountDataModel );

        _logger.LogWarning ( "User account creation result for email: {Email} is {Result}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty, result );

        if ( result )
        {
            var emailVerifyToken = await _userAccountService.GetEmailVerifyToken ( accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

            _logger.LogWarning ( "Email verification token for email: {Email} is {Token}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty, emailVerifyToken );


            if (!string.IsNullOrEmpty(emailVerifyToken))
            {

                var encodedToken = HttpUtility.UrlEncode(emailVerifyToken);

                ////VerifyEmailDataModel verifyEmailDataModel = new VerifyEmailDataModel(accountDisplayViewModel.Email, emailVerifyToken);

                ////verifyEmailDataModel.Subject = "Please, first Verify Your Email to Login.";

                //    verifyEmailDataModel.VerifyLink =
                //        Url.Action ( "VerifyEmail","Auth",new
                //        {
                //            email = accountDisplayViewModel.Email,
                //            token = encodedToken
                //        }, protocol:
                //Request.Scheme );

                ////await _emailService.SendEmailVerificationAsync(verifyEmailDataModel);

                ////return RedirectToAction ( "VerifyEmail", verifyEmailDataModel );
                ///

            _logger.LogWarning ( "Redirecting to VerifyEmail action for email: {Email} with token: {Token}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty,encodedToken );

                return RedirectToAction ( "VerifyEmail", new
            {
                email = accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty,
                token = encodedToken
            } );

            }
        }

        _logger.LogWarning ( "Failed to create user account for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

        return RedirectToAction ( "Signup" );
    }
    

    public IActionResult Login()
    {
        var loginDisplayViewModel = new LoginDisplayViewModel("Sign in");

        return View(loginDisplayViewModel);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login( LoginDisplayViewModel loginDisplayViewModel )
    {
        if ( !ModelState.IsValid )
        {
            _logger.LogWarning ( "Invalid login attempt for email: {Email}", loginDisplayViewModel.Email );

            return RedirectToAction ( "Login" );
        }

        var result = await _userAccountService.AuthenticateUser ( loginDisplayViewModel.Email, loginDisplayViewModel.Password );

        _logger.LogWarning ( "Authentication result for email: {Email} is {Result}", loginDisplayViewModel.Email, result.Succeeded );

        if (result.Succeeded)
        {

            int userID = await _userAccountService.GetSingleUser(loginDisplayViewModel.Email);

            _logger.LogWarning ( "Retrieved user ID for email: {Email} is {UserID}", loginDisplayViewModel.Email, userID );

            if (userID == 0)
            {
                _logger.LogWarning ( "Failed to retrieve user ID for email: {Email}", loginDisplayViewModel.Email );

                return RedirectToAction ( "Login" );
            }

           
            var claims = new List<Claim> {

                new ( ClaimTypes.Name, loginDisplayViewModel.Email ) ,
                new ( ClaimTypes.NameIdentifier, userID.ToString() )

            };   

            var identity = new ClaimsIdentity ( claims, IdentityConstants.ApplicationScheme  );

            var principal = new ClaimsPrincipal (identity);

            _logger.LogWarning ( "Signing in user for email: {Email}", loginDisplayViewModel.Email );

            await HttpContext.SignInAsync ( IdentityConstants.ApplicationScheme, principal );


            _logger.LogWarning ( "User signed in successfully for email: {Email}", loginDisplayViewModel.Email );

            return RedirectToAction ("Index", "Home");
        }

        if ( result.IsNotAllowed )
        {
            ModelState.AddModelError ( string.Empty, "You must confirm your email before logging in." );

            _logger.LogWarning ( "User attempted to log in without confirming email: {Email}", loginDisplayViewModel.Email );

            return View ( loginDisplayViewModel );
        }

        if ( result.IsLockedOut )
        {
            _logger.LogWarning ( "User account is locked out for email: {Email}",loginDisplayViewModel.Email );

            return View ( "Lockout" );
        }

        _logger.LogWarning ( "Invalid login attempt for email: {Email}", loginDisplayViewModel.Email );

        ModelState.AddModelError ( string.Empty, "Invalid login attempt." );

        _logger.LogWarning ( "Invalid login attempt for email: {Email}", loginDisplayViewModel.Email );

        return RedirectToAction ( "Login" );
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        _logger.LogWarning ( "User signed out for email: {Email}",HttpContext != null && HttpContext.User != null && HttpContext.User.Identity != null ? HttpContext.User.Identity.Name : "Unknown" );

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
                _logger.LogWarning ( "Password reset successful for email: {Email}", accountDisplayViewModel.Email );

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
}
