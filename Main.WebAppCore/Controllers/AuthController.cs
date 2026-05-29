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
        if ( string.IsNullOrEmpty ( email ) || string.IsNullOrEmpty ( token ) )
        {
            return BadRequest ( "Invalid verification request parameters." );
        }

        var result = await _userAccountService.CreateAppicationUser ( email, token );


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
            return RedirectToAction ( "Signup" );
        }


        if(!AuthExtensions.CheckPasswordMatch ( accountDisplayViewModel.Password, accountDisplayViewModel.RePassword ) )
        {
            _logger.LogWarning ( "Password and RePassword do not match for email: {Email}",accountDisplayViewModel.Email );

            return RedirectToAction ( "Signup" );
        }


        UserAccountDataModel userAccountDataModel 
            = AuthExtensions.MapToDataModel (accountDisplayViewModel);


        bool result 
            = await _userAccountService.CreateIdentityUserAccount ( userAccountDataModel );


        if ( result )
        {
            var emailVerifyToken = await _userAccountService.GetEmailVerifyToken ( accountDisplayViewModel.Email.Trim() );


            if(!string.IsNullOrEmpty(emailVerifyToken))
            {
                var encodedToken = HttpUtility.UrlEncode(emailVerifyToken);

                VerifyEmailDataModel verifyEmailDataModel = new VerifyEmailDataModel(accountDisplayViewModel.Email, emailVerifyToken);

                verifyEmailDataModel.Subject = "Please, first Verify Your Email to Login.";
                
                verifyEmailDataModel.VerifyLink = 
                    Url.Action ( "VerifyEmail","Auth", new {
                        email = accountDisplayViewModel.Email,
                        token = encodedToken
                    }, protocol: Request.Scheme ); 

                await _emailService.SendEmailVerificationAsync(verifyEmailDataModel);

                return RedirectToAction ( "VerifyEmail", verifyEmailDataModel );
            }
        }

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
        if ( ModelState.IsValid )
        {
            return RedirectToAction ( "Login" );
        }

        var result = await _userAccountService.AuthenticateUser ( loginDisplayViewModel.Email, loginDisplayViewModel.Password );


        if (result)
        {

            int userID = await _userAccountService.GetSingleUser(loginDisplayViewModel.Email);

            if (userID == 0)
            {
                return RedirectToAction ( "Login" );
            }

           
            var claims = new List<Claim> {

                new Claim ( ClaimTypes.Name, loginDisplayViewModel.Email ) ,
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
}
