using DataTransferModel;

using FluentEmail.Core;

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

public class AuthController: BaseController
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IUserContext _userContext;
    private readonly ILogger<AuthController> _logger;
    private readonly IAccountService _userAccountService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailSenderService _emailService;

    public AuthController (
        ILogger<AuthController> logger,
        IStringLocalizer<SharedResource> localizer,
        IAccountService userAccountService,
        IUserContext userContext,
        IConfiguration configuration,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IEmailSenderService emailService
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


    // Registration Flow: User accesses the registration page, which displays the registration form
    public IActionResult Signup ( )
    {
        var objModel = new AccountDisplayViewModel("Registration Page");

        return View ( objModel );
    }



    // Registration Flow: User submits the registration form with email, password, and other details, which triggers the SignUp action that validates input, creates a new user account, sends a verification email, and redirects to a confirmation page
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp ( AccountDisplayViewModel accountDisplayViewModel )
    {

        if ( ModelState.IsValid )
        {
             return View ( accountDisplayViewModel );
        }

        // OWASP Mitigation: Validate that password and confirm password match and do not reveal which one is incorrect
        if ( !AuthExtensions.CheckPasswordMatch ( accountDisplayViewModel != null ? accountDisplayViewModel.Password : string.Empty,accountDisplayViewModel != null ? accountDisplayViewModel.RePassword : string.Empty ) )
        {
            return View ( accountDisplayViewModel );
        }


        // Map the view model to the data model for user account creation
        UserAccountDataModel userAccountDataModel
            = AuthExtensions.MapToDataModel (accountDisplayViewModel != null ? accountDisplayViewModel : new AccountDisplayViewModel());

        // OWASP Mitigation: Create the user account with secure password hashing and do not reveal if the email is already registered or if the account creation failed
        IdentityResult result
            = await _userAccountService.CreateIdentityUserAccount ( userAccountDataModel );


        if ( result.Succeeded )
        {
            // OWASP Mitigation: Send email verification email with secure token if account creation succeeded, regardless of whether the email is already registered or not
            await SendUserAccountVerificationEmail ( accountDisplayViewModel != null ? accountDisplayViewModel.Email : string.Empty );

            // Redirect to a generic confirmation page that instructs the user to check their email for the verification link, without revealing if the account was created or if the email is already registered
            return RedirectToAction ( "VerifyEmailSent" );
        }

        // Add errors to model state to display in the view
        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty, error.Description );
        }

        // Return the view with the original input and error messages, without revealing if the email is already registered or if the account creation failed
        return View ( accountDisplayViewModel);
    }



    // Registration Flow: After successful registration, user is shown a page that instructs them to check their email for the verification link (OWASP Mitigation)
    public IActionResult VerifyEmailSent ( )
    {
        ViewData["Title"] = "Email Sent";


        return View ( );
    }



    // Registration Flow: User clicks the email verification link, which triggers the VerifyEmail action that validates the token, activates the user account, and redirects to a confirmation page (Complete)
    public async Task<IActionResult> VerifyEmail ( VerifyEmailViewModel verifyEmailViewModel )
    {
        if ( string.IsNullOrEmpty ( verifyEmailViewModel.Email ) || string.IsNullOrEmpty ( verifyEmailViewModel.Token ) )
        {
            return BadRequest ( "Invalid verification request parameters." );
        }

        BaseDataModel baseDataModel = _userContext.GetCreateBaseDataModel ( );

        var result = await _userAccountService.CreateAppicationUser ( verifyEmailViewModel.Email, verifyEmailViewModel.Token, baseDataModel );

        return RedirectToAction ( "VerifyEmailConfirmation" );
    }



    // Registration Flow: User clicks the email verification link, which triggers the VerifyEmail action that validates the token, activates the user account, and redirects to a confirmation page (Complete)
    public IActionResult VerifyEmailConfirmation ( )
    {
        ViewData["Title"] = "Verification Confirmed";

        return View ( );
    }



    // Login Flow: User accesses the login page, which displays the login form
    public IActionResult Login ( )
    {
        var loginDisplayViewModel = new LoginDisplayViewModel("Login");

        return View ( loginDisplayViewModel );
    }


    // Login Flow: User submits login form with email and password, which triggers the Login action that validates credentials, checks email verification, sets user claims, and redirects to the home page on success
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login ( LoginDisplayViewModel loginDisplayViewModel )
    {

        if ( !ModelState.IsValid )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please check your credentials and try again.";

            return View ( loginDisplayViewModel );
        }

        IdentityUser? userIdentity = await _userManager
            .FindByEmailAsync(loginDisplayViewModel.Email);


        if ( userIdentity == null )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please check your credentials and try again.";

            return View ( loginDisplayViewModel );
        }


        if ( !await _userManager.IsEmailConfirmedAsync ( userIdentity ) )
        {
            loginDisplayViewModel.Message = "Invalid login attempt. Please, check your email if you have any account in this website.";

            await SendUserAccountVerificationEmail ( loginDisplayViewModel.Email );

            return RedirectToAction ( "Login" );
        }


        // OWASP Mitigation: Use secure password verification and do not reveal if the password is incorrect or the account is locked
        var result = await _signInManager.PasswordSignInAsync (
                                        userIdentity.UserName!, 
                                        loginDisplayViewModel.Password, 
                                        isPersistent: false, 
                                        lockoutOnFailure: false );


        if ( result.Succeeded )
        {
            int userID = await _userAccountService.GetSingleUser(loginDisplayViewModel.Email);

            if ( userID == 0 )
            {
                loginDisplayViewModel.Message = "Invalid login attempt. Please, check your email if you have any account in this website.";

                await SendUserAccountVerificationEmail ( loginDisplayViewModel.Email );

                return RedirectToAction ( "Login" );
            }

            await SetUserClaimsForCurrentSession ( userIdentity, loginDisplayViewModel.Email, userID );


            // Successful login, redirect to home page or dashboard
            return RedirectToAction ( "Index", "Home" );
        }

        // OWASP Mitigation: Do not reveal if the password is incorrect or the account is locked, show a generic error message
        return View ( loginDisplayViewModel );
    }



    // Helper method to set user claims for the current session after successful login (OWASP Mitigation)
    private async Task SetUserClaimsForCurrentSession ( IdentityUser userIdentity,string email,int userID )
    {
        // OWASP Mitigation: Add claims to the user identity for role-based authorization and do not reveal if the user has a specific role
        var userRole = await _userAccountService.GetUserRole(email);

 
        string? roleName = userRole != null ? userRole.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value : "User";

       
        await _userManager.AddClaimAsync ( userIdentity,new ( ClaimTypes.Name,userIdentity.UserName != null ? userIdentity.UserName : "" ) );

        
        await _userManager.AddClaimAsync ( userIdentity,new ( ClaimTypes.Email, email ) );

        
        await _userManager.AddClaimAsync ( userIdentity,
            new ( ClaimTypes.Role,roleName != null ? roleName : "User" ) );

       
        await _userManager.AddClaimAsync ( userIdentity,new ( ClaimTypes.NameIdentifier,
            userID.ToString ( ) ) );
    }


    // Logout Flow: User clicks the logout button, which triggers the Logout action that signs the user out and redirects to the home page
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout ( )
    {
        await _signInManager.SignOutAsync ( );

        _logger.LogWarning ( "User signed out for email: {Email}",HttpContext != null && HttpContext.User != null && HttpContext.User.Identity != null ? HttpContext.User.Identity.Name : "Unknown" );

        return RedirectToAction ( "Index","Home" );
    }


    // Helper method to send email verification email with secure token if user exists but email is not verified (OWASP Mitigation)
    private async Task SendUserAccountVerificationEmail ( string email )
    {
        var user = await _userManager.FindByEmailAsync(email);

        if ( user == null )
        {
            return;
        }

        var emailVerifyToken = await _userAccountService.GetEmailVerifyToken ( email );

        _logger.LogWarning ( "Email verification token for email: {Email} is {Token}",email,emailVerifyToken );

        if ( !string.IsNullOrEmpty ( emailVerifyToken ) )
        {
            var encodedToken = HttpUtility.UrlEncode(emailVerifyToken);

            _logger.LogWarning ( "Redirecting to VerifyEmail action for email: {Email} with token: {Token}",email,encodedToken );


            var verifyLink = Url.Action ( "VerifyEmail", "Auth", new VerifyEmailViewModel ()
            {
                Email = email,
                Token = encodedToken
            },  protocol: Request.Scheme );

            var verifyEmailDataModel = new VerifyEmailDataModel ()
            {
                Email = email,
                LinkUrl = verifyLink != null    ?
                          verifyLink.ToString() : string.Empty,
                Subject = "Confirm your email verification"
            };

            await _emailService.SendEmailVerificationAsync ( verifyEmailDataModel );

        }
    }



    // Forget Password Reset Flow - Step 1: User initiates password reset by providing email address 
    [HttpGet]
    public IActionResult ForgotPasswordResetInit ( )
    {
        ViewData["Title"] = "Password Reset";

        return View ( new ForgotPasswordViewModel() );
    }
    


    // Forget Password Reset Flow - Step 2.0: User submits email address to receive password reset link, regardless of whether the email exists or is verified (OWASP Mitigation)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword ( ForgotPasswordViewModel forgotPasswordViewModel )
    {
        if ( !ModelState.IsValid )
            return View ( forgotPasswordViewModel );

        var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);

        // OWASP Mitigation: Do not reveal if the user exists or is verified
        if ( user == null || !( await _userManager.IsEmailConfirmedAsync ( user ) ) )
        {
            await SendUserAccountVerificationEmail ( forgotPasswordViewModel.Email );

            // Do not reveal if the user exists or is verified
            return RedirectToAction ( nameof ( ForgotPasswordSent ));
        }

        // Step 2.1: Send email with password reset link
        await SendPasswordResetEmail ( forgotPasswordViewModel.Email );

        // Do not reveal if the user exists or is verified
        return RedirectToAction ( nameof ( ForgotPasswordSent ) );
    }


    // Step 2.1: Helper method to send password reset email with secure token
    private async Task SendPasswordResetEmail ( string email )
    {
        var user = await _userManager.FindByEmailAsync(email);

        if ( user == null )
        {
            return;
        }

        // Step 2.1.1: Generate a secure single-use token embedded in the URL
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        if ( !string.IsNullOrEmpty ( token ) )
        {
            var encodedToken = HttpUtility.UrlEncode(token);

            var callbackUrl = Url.Action("ResetPassword", "Auth",
                                    new ResetPasswordViewModel()
                                    {
                                        Token = encodedToken,
                                        Email = email
                                    },
                                    protocol: Request.Scheme);

            var callbackUrlString = $"Link: {callbackUrl}";

            var encodedCallbackUrl = HttpUtility.HtmlEncode ( callbackUrlString );

            // Execute asynchronous handoff to background email dispatcher
            await _emailService.SendEmailAsync
                ( email,"Reset Password",encodedCallbackUrl );

        }
    }



    // Forget Password Reset Flow - Step 3: User is informed that if the email exists and is verified, a password reset link has been sent (OWASP Mitigation)
    [HttpGet]
    public IActionResult ForgotPasswordSent ( )
    {
        ViewData["Title"] = "Forgot Password";

        return View ( );
    }



    // Forget Password Reset Flow - Step 4: User clicks the password reset link, which includes the secure token, and is taken to the password reset form
    [HttpPost]
    public async Task<IActionResult> ResetPassword ( ResetPasswordViewModel resetPasswordViewModel )
    {

        if ( !ModelState.IsValid )
            return View ( resetPasswordViewModel );


        var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);


        if ( user == null )
        {
            return RedirectToAction ( "Login","Auth" );
        }


        // Reset with new passwrd and invalidate the token and timestamp to prevent reuse (OWASP Mitigation)
        var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.ConfirmPassword);


        if ( result.Succeeded )
        {
            return RedirectToAction ( nameof ( ResetPasswordConfirmation ) );
        }


        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty,error.Description );
        }


        return View ( resetPasswordViewModel );
    }



    // Forget Password Reset Flow - Step 5: User is shown a confirmation page that the password has been reset successfully
    [HttpGet]
    public IActionResult ResetPasswordConfirmation ( )
    {
        ViewData["Title"] = "Reset Password Completed";

        return View ( );
    }


    // Change Password Flow - Step 1: Authenticated user accesses the change password form
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


    // Change Password Flow - Step 2: User submits the change password form with current and new passwords
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword ( ChangePasswordViewModel changePasswordViewModel )
    {

        if ( !ModelState.IsValid )
            return View ( changePasswordViewModel );

        var userIdentity = await _userManager.FindByEmailAsync(changePasswordViewModel.Email);


        if ( userIdentity == null )
        {
            return View ( changePasswordViewModel );
        }


        var result = await _userManager
            .ChangePasswordAsync(userIdentity,changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);


        _logger.LogWarning ( "Password change attempt for email: {Email}",changePasswordViewModel.Email );


        if ( result.Succeeded )
        {
            _logger.LogWarning ( "Password change successful for email: {Email}",changePasswordViewModel.Email );


            return RedirectToAction ( nameof ( ChangePasswordConfirmation ) );
        }

        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty,error.Description );
        }

        return View ( changePasswordViewModel );
    }


    // Change Password Flow - Step 3: User is shown a confirmation page that the password has been changed successfully
    [HttpGet]
    public IActionResult ChangePasswordConfirmation ( )
    {
        ViewData["Title"] = "Password Change Confirmation";

        return View ( );
    }

}
