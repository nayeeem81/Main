using Main.Common.Model;
using Main.Services;
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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController (
        ILogger<AuthController> logger,
        IStringLocalizer<SharedResource> localizer,
        IAccountService userAccountService,
        IEmailSenderService emailService,
        IUserContext userContext,
        IConfiguration configuration,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager
       )
    {
        _userAccountService = userAccountService;
        _localizer = localizer;
        _logger = logger;
        _userContext = userContext;
        _emailService = emailService;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Signup()
    {
        var objModel = new AccountDisplayViewModel("Registration Page");
               
        return View(objModel);
    }

    public async Task<IActionResult> VerifyEmail ( string email, string token )
    {
        _logger.LogWarning ( "Verifying email for address: {Email} with token: {Token}", email, token );

        if ( string.IsNullOrEmpty ( email ) || string.IsNullOrEmpty ( token ) )
        {
            return BadRequest ( "Invalid verification request parameters." );
        }

        _logger.LogWarning ( "Attempting to create application user for email: {Email} with token: {Token}", email, token );

        BaseDataModel baseDataModel = _userContext.GetCreateBaseDataModel ( );

        var result = await _userAccountService.CreateAppicationUser ( email, token, baseDataModel );

        _logger.LogWarning ( "Application user creation result for email: {Email} with token: {Token} is {Result}", email, token, result );

        return RedirectToAction ( "Login" );
    }


    //public IActionResult VerifyEmail ( VerifyEmailViewModel verifyEmailViewModel )
    //{
    //    return View ( verifyEmailViewModel );
    //}


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp ( AccountDisplayViewModel accountDisplayViewModel )
    {

        if ( ModelState.IsValid )
        {
            _logger.LogWarning ( "Validation: Invalid for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

            return View ( accountDisplayViewModel );
        }


        if ( !AuthExtensions.CheckPasswordMatch ( accountDisplayViewModel != null ? accountDisplayViewModel.Password : string.Empty,accountDisplayViewModel != null ? accountDisplayViewModel.RePassword : string.Empty ) )
        {
            _logger.LogWarning ( "Password and RePassword do not match for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

            return RedirectToAction ( "Signup" );
        }


        UserAccountDataModel userAccountDataModel
            = AuthExtensions.MapToDataModel (accountDisplayViewModel != null ? accountDisplayViewModel : new AccountDisplayViewModel());

        _logger.LogWarning ( "Mapping (Data Model) completed for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

        IdentityResult result
            = await _userAccountService.CreateIdentityUserAccount ( userAccountDataModel );

        _logger.LogWarning ( "User account creation result for email: {Email} is {Result}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty,result );

        if ( result.Succeeded )
        {
            var emailVerifyToken = await _userAccountService.GetEmailVerifyToken ( accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

            _logger.LogWarning ( "Email verification token for email: {Email} is {Token}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty,emailVerifyToken );


            if ( !string.IsNullOrEmpty ( emailVerifyToken ) )
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

                return RedirectToAction ( "VerifyEmail",new
                {
                    email = accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty,
                    token = encodedToken
                } );

            }
        }
        else
        {
            _logger.LogWarning ( "User account creation failed for email: {Email} with errors: {Errors}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty,string.Join ( ", ",result.Errors.Select ( e => e.Description ) ) );
        }


        _logger.LogWarning ( "Failed to create user account for email: {Email}",accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

        return RedirectToAction ( "Signup" );
    }
    

    public IActionResult Login()
    {
        var loginDisplayViewModel = new LoginDisplayViewModel("Login");

        return View(loginDisplayViewModel);
    }



    [HttpPost]
    public async Task<IActionResult> Login ( LoginDisplayViewModel loginDisplayViewModel )
    {
        if ( ModelState.IsValid )
            return View ( loginDisplayViewModel );

        var user = await _userManager.FindByEmailAsync(loginDisplayViewModel.Email);

        if ( user == null )
        {
            ModelState.AddModelError ( "","Invalid login attempt." );
            loginDisplayViewModel.Message = "Invalid login attempt. Your accont is locked. Please, contant suport.";
            return View ( loginDisplayViewModel );
        }

        if ( !await _userManager.IsEmailConfirmedAsync ( user ) )
        {
            ModelState.AddModelError ( "","You must verify your email before logging in." );
            loginDisplayViewModel.Message = "You must verify your email before logging in. Please, check your email for verification link.";

            return View ( loginDisplayViewModel );
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName!, loginDisplayViewModel.Password, isPersistent: false, lockoutOnFailure: false);


        if ( result.Succeeded )
        {
            int userID = await _userAccountService.GetSingleUser(loginDisplayViewModel.Email);

            _logger.LogWarning ( "Retrieved user ID for email: {Email} is {UserID}",loginDisplayViewModel.Email,userID );

            if ( userID == 0 )
            {
                _logger.LogWarning ( "Failed to retrieve user ID for email: {Email}",loginDisplayViewModel.Email );

                return RedirectToAction ( "Login" );
            }

            var userRole = await _userAccountService.GetUserRole(loginDisplayViewModel.Email);

            string? roleName = userRole != null ? userRole.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value : "User";

            
            await _userManager.AddClaimAsync ( user,new ( ClaimTypes.Name, user.UserName != null ? user.UserName : "" ) );

            await _userManager.AddClaimAsync ( user,new ( ClaimTypes.Email,loginDisplayViewModel.Email ) );

            await _userManager.AddClaimAsync ( user,
                new ( ClaimTypes.Role,roleName != null ? roleName : "User" ) );

            await _userManager.AddClaimAsync ( user,new ( ClaimTypes.NameIdentifier, user.Id ) );

            _logger.LogWarning ( "User signed in successfully for email: {Email}",loginDisplayViewModel.Email );

            return RedirectToAction ( "Index","Home" );
        }

        return View ( loginDisplayViewModel );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync ( );

        _logger.LogWarning ( "User signed out for email: {Email}",HttpContext != null && HttpContext.User != null && HttpContext.User.Identity != null ? HttpContext.User.Identity.Name : "Unknown" );

        return RedirectToAction("Index", "Home");
    }



    [HttpGet]
    public IActionResult ForgotPassword ( ) 
    {
        var model = new ForgotPasswordViewModel();

        ViewData["Title"] = "Forgot Password";

        return View(model);
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword ( ForgotPasswordViewModel model )
    {
        if ( !ModelState.IsValid )
            return View ( model );

        var user = await _userManager.FindByEmailAsync(model.Email);

        // OWASP Mitigation: Do not reveal if the user exists or is verified
        if ( user == null || !( await _userManager.IsEmailConfirmedAsync ( user ) ) )
        {
            return RedirectToAction ( nameof ( ForgotPasswordConfirmation ) );
        }

        // Generate a secure single-use token embedded in the URL
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var callbackUrl = Url.Action("ResetPassword", "Auth",
                new
                {
                    token, email = model.Email
                }, protocol: Request.Scheme);

        // Execute asynchronous handoff to background email dispatcher
        // Example: await _emailSender.SendEmailAsync(model.Email, "Reset Password", $"Link: {callbackUrl}");

        return RedirectToAction ( nameof ( ForgotPasswordConfirmation ) );
    }



    [HttpGet]
    public IActionResult ForgotPasswordConfirmation ( )
    {
        ViewData["Title"] = "Forgot Password Confirmation";

        return View ( );
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation ( )
    {
        ViewData["Title"] = "Reset Password Confirmation";

        return View ( );
    }


    [HttpGet]
    public async Task<IActionResult> ChangePassword ( )
    {
        if ( User == null )
        {
            return RedirectToAction ( "Login","Auth" );
        }

        var changePasswordViewModel = new ChangePasswordViewModel();

        changePasswordViewModel.Email = User.Claims.FirstOrDefault ( c => c.Type == ClaimTypes.Email )?.Value ?? "";


        return View ( changePasswordViewModel );
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword ( ChangePasswordViewModel changePasswordViewModel )
    {
        if ( ! ModelState.IsValid )
            return View ( changePasswordViewModel );

        var userIdentity = await _userManager.FindByEmailAsync(changePasswordViewModel.Email);


        if ( userIdentity == null )
        {
            return View ( changePasswordViewModel );
        }

        
        var result = await _userManager
            .ChangePasswordAsync(userIdentity,changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
                                          
        _logger.LogWarning ("Password change attempt for email: {Email}", changePasswordViewModel.Email);

        if ( result.Succeeded )
        {
            _logger.LogWarning ( "Password change successful for email: {Email}", changePasswordViewModel.Email );

            return RedirectToAction ( nameof ( ChangePasswordConfirmation ) );
        }

        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty, error.Description );
        }

        return View ( changePasswordViewModel );
    }



    [HttpGet]
    public IActionResult ChangePasswordConfirmation ( )
    {
        ViewData["Title"] = "Password Change Confirmation";

        return View ( );
    }

}
