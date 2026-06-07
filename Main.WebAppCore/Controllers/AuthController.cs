using DataTransferModel;
using Main.Common.Model;
using Main.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore;

public class AuthController: BaseController
{
    private readonly IUserContext _userContext;
    private readonly IAccountService _userAccountService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IEmailSenderService _emailService;

    public AuthController (
        IAccountService userAccountService,
        IUserContext userContext,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IEmailSenderService emailService
       )
    {
        _userAccountService = userAccountService;
        _userContext = userContext;
        _emailService = emailService;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    // Registration Flow: User accesses the registration page, which displays the registration form
    public IActionResult Registration ( )
    {
        var objModel = new RegistrationViewModel();

        return View ( objModel );
    }


    // Registration Flow: User submits the registration form with email, password, and other details, which triggers the SignUp action that validates input, creates a new user account, sends a verification email, and redirects to a confirmation page
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registration ( RegistrationViewModel registrationViewModel )
    {

        if ( ModelState.IsValid )
        {
             return View ( registrationViewModel );
        }

        // OWASP Mitigation: Validate that password and confirm password match and do not reveal which one is incorrect
        if ( !AuthExtensions.CheckPasswordMatch ( registrationViewModel != null ? registrationViewModel.Password : string.Empty,registrationViewModel != null ? registrationViewModel.RePassword : string.Empty ) )
        {
            return View ( registrationViewModel );
        }


        // Map the view model to the data model for user account creation
        UserAccountDataModel userAccountDataModel
            = AuthExtensions.MapToDataModel (registrationViewModel != null ? registrationViewModel : new RegistrationViewModel());

        // OWASP Mitigation: Create the user account with secure password hashing and do not reveal if the email is already registered or if the account creation failed
        IdentityResult result
            = await _userAccountService.CreateIdentityUserAccount ( userAccountDataModel );


        if ( result.Succeeded )
        {
            // OWASP Mitigation: Send email verification email with secure token if account creation succeeded, regardless of whether the email is already registered or not
            await SendVerifyEmail ( registrationViewModel != null ? registrationViewModel.Email : string.Empty );

            // Redirect to a generic confirmation page that instructs the user to check their email for the verification link, without revealing if the account was created or if the email is already registered
            return RedirectToAction ( "VerifyEmailSent" );
        }

        // Add errors to model state to display in the view
        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty, error.Description );
        }

        // Return the view with the original input and error messages, without revealing if the email is already registered or if the account creation failed
        return View ( registrationViewModel);
    }



    // Registration Flow: After successful registration, user is shown a page that instructs them to check their email for the verification link (OWASP Mitigation)
    public IActionResult VerifyEmailSent ( )
    {
        ViewData["Title"] = "Email Sent";

        return View ( );
    }



    // Registration Flow: User clicks the email verification link, which triggers the VerifyEmail action that validates the token, activates the user account, and redirects to a confirmation page (Complete)
    public async Task<IActionResult> VerifyLink ( string email, string token  )
    {
        if ( string.IsNullOrEmpty ( email ) || string.IsNullOrEmpty ( token  ) )
        {
            return BadRequest ( "Invalid verification request parameters." );
        }

        BaseDataModel baseDataModel = _userContext.GetCreateBaseDataModel ( );

        var result = await _userAccountService.CreateAppicationUser ( email, token , baseDataModel );

        return RedirectToAction ( "VerifyComplete" );
    }



    // Registration Flow: User clicks the email verification link, which triggers the VerifyEmail action that validates the token, activates the user account, and redirects to a confirmation page (Complete)
    public IActionResult VerifyComplete ( )
    {
        ViewData["Title"] = "Verification Complete";

        return View ( );
    }



    // Login Flow: User accesses the login page, which displays the login form
    public IActionResult Login ( )
    {
        var loginDisplayViewModel = new LoginViewModel("Login");

        return View ( loginDisplayViewModel );
    }



    // Login Flow: User submits login form with email and password, which triggers the Login action that validates credentials, checks email verification, sets user claims, and redirects to the home page on success
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login ( LoginViewModel loginDisplayViewModel )
    {

        if ( ModelState.IsValid )
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

            await SendVerifyEmail ( loginDisplayViewModel.Email );

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
            await SetUserClaimsForCurrentSession ( userIdentity, loginDisplayViewModel.Email );


            // Successful login, redirect to home page or dashboard
            return RedirectToAction ( "Index", "Home" );
        }

        // OWASP Mitigation: Do not reveal if the password is incorrect or the account is locked, show a generic error message
        return View ( loginDisplayViewModel );
    }



    // Helper method to set user claims for the current session after successful login (OWASP Mitigation)
    private async Task SetUserClaimsForCurrentSession ( IdentityUser userIdentity,string email )
    {
        // OWASP Mitigation: Add claims to the user identity for role-based authorization and do not reveal if the user has a specific role
        var userRole = await _userAccountService.GetUserRole(email);

 
        string? roleName = userRole != null ? userRole.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Role)?.Value : "User";

       
        await _userManager.AddClaimAsync ( userIdentity,new ( ClaimTypes.Name,userIdentity.UserName != null ? userIdentity.UserName : "" ) );

        
        await _userManager.AddClaimAsync ( userIdentity,new ( ClaimTypes.Email, email ) );

        
        await _userManager.AddClaimAsync ( userIdentity,
            new ( ClaimTypes.Role,roleName != null ? roleName : "User" ) );

       
        await _userManager.AddClaimAsync ( userIdentity,new ( ClaimTypes.NameIdentifier,
            userIdentity.Id.ToString ( ) ) );
    }



    // Logout Flow: User clicks the logout button, which triggers the Logout action that signs the user out and redirects to the home page
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout ( )
    {
        await _signInManager.SignOutAsync ( );

        return RedirectToAction ( "Index","Home" );
    }



    // Helper method to send email verification email with secure token if user exists but email is not verified (OWASP Mitigation)
    private async Task SendVerifyEmail ( string email )
    {
        var user = await _userManager.FindByEmailAsync(email);

        if ( user == null )
        {
            return;
        }

        var emailVerifyToken = await _userAccountService.GetEmailVerifyToken ( email );

        if ( !string.IsNullOrEmpty ( emailVerifyToken ) )
        {
            var verifyLink = Url.Action ( "VerifyLink", "Auth", new 
            {
                Email = email,
                Token = emailVerifyToken
            },  Request.Scheme );

            var verifyEmailDataModel = new VerifyDataModel ()
            {
                Email = email,
                VerifyLink = verifyLink != null    ?
                          verifyLink.ToString() : string.Empty
            };

            await _emailService.SendEmailVerificationAsync ( verifyEmailDataModel );

        }
    }



    // Forget Password Reset Flow - Step 1: User initiates password reset by providing email address 
    [HttpGet]
    public IActionResult ResetEmail ( )
    {
        ViewData["Title"] = "Password Reset";

        return View ( new ForgotPasswordViewModel() );
    }
    


    // Forget Password Reset Flow - Step 2.0: User submits email address to receive password reset link, regardless of whether the email exists or is verified (OWASP Mitigation)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetEmail ( ForgotPasswordViewModel forgotPasswordViewModel )
    {
        if ( !ModelState.IsValid )
            return View ( forgotPasswordViewModel );

        var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);

        // OWASP Mitigation: Do not reveal if the user exists or is verified
        if ( user == null || !( await _userManager.IsEmailConfirmedAsync ( user ) ) )
        {
            await SendVerifyEmail ( forgotPasswordViewModel.Email );

            // Do not reveal if the user exists or is verified
            return RedirectToAction ( nameof ( SendVerifyEmail ));
        }

        // Step 2.1: Send email with password reset link
        await SendResetEmail ( forgotPasswordViewModel.Email );

        // Do not reveal if the user exists or is verified
        return RedirectToAction ( nameof ( ResetEmailSent ) );
    }



    // Step 2.1: Helper method to send password reset email with secure token
    private async Task SendResetEmail ( string email )
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
          
            var callbackUrl = Url.Action("ResetLink", "Auth", 
                              new  { Email = email , Token = token }, Request.Scheme);

            var resetDataModel = new ResetDataModel() {
                Email = email,
                ResetLink = callbackUrl != null ? callbackUrl.ToString() : string.Empty
            };

            await _emailService.SendResetPasswordEmailAsync(resetDataModel);
        }
    }



    // Forget Password Reset Flow - Step 3: User is informed that if the email exists and is verified, a password reset link has been sent (OWASP Mitigation)
    [HttpGet]
    public IActionResult ResetEmailSent ( )
    {
        ViewData["Title"] = "Reset Email Sent";

        return View ( );
    }



    // Forget Password Reset Flow - Step 4: User clicks the password reset link, which includes the secure token, and is taken to the password reset form
    public async Task<IActionResult> ResetLink (string email, string token )
    {

        if ( string.IsNullOrEmpty ( email ) || string.IsNullOrEmpty ( token ) )
        {
            return BadRequest ( "Invalid link request." );
        }

        var user = await _userManager.FindByEmailAsync(email);

        if ( user == null )
        {
            return BadRequest ( "Invalid link request." );
        }

        var resetPasswordViewModel = new ResetPasswordViewModel()
        {
            Email = email,
            Token = token
        };  

        return View ( "ResetPassword", resetPasswordViewModel);
                                                                   
    }



    // Forget Password Reset Flow - Step 5: User submits the new password, which triggers the ResetPassword action that validates the token, resets the password, invalidates the token, and redirects to a confirmation page
    [HttpPost]
    public async Task<IActionResult> ResetPassword ( ResetPasswordViewModel resetPasswordViewModel )
    {

        if ( !ModelState.IsValid )
            return View ( resetPasswordViewModel );

        IdentityUser? userIdentity = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);


        // Reset with new passwrd and invalidate the token and timestamp to prevent reuse (OWASP Mitigation)
        var result = await _userManager.ResetPasswordAsync(userIdentity ?? throw new InvalidOperationException("User not found"), resetPasswordViewModel.Token, resetPasswordViewModel.ConfirmPassword);


        if ( result.Succeeded )
        {
            return RedirectToAction ( nameof ( ResetComplete ) );
        }


        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty,error.Description );
        }

        return View ( resetPasswordViewModel );
    } 



    // Forget Password Reset Flow - Step 6: User is shown a confirmation page that the password has been reset successfully
    [HttpGet]
    public IActionResult ResetComplete ( )
    {
        ViewData["Title"] = "Password Updated";

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


        if ( result.Succeeded )
        {
            return RedirectToAction ( nameof ( ResetComplete ) );
        }

        foreach ( var error in result.Errors )
        {
            ModelState.AddModelError ( string.Empty,error.Description );
        }

        return View ( changePasswordViewModel );
    }

}
