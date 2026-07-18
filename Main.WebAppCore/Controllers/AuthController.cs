using DataTransferModel;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Services;
using Main.WebAppCore.Controllers.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppCore.ViewModel;
using WebAppCore.ViewModel.Extensions;

namespace Main.WebAppCore.Controllers;

public class AuthController: BaseController
{
    private readonly ITenantSetter _tenantSetter;
    private readonly ITenantContext _userContext;
    private readonly IAccountService _userAccountService;
    private readonly IEmailSenderService _emailService;
    private readonly ITokenService _tokenService;

    public AuthController (
        IAccountService userAccountService,
        ITenantContext userContext,
        IEmailSenderService emailService,
        ITenantSetter tenantSetter,
        ITokenService tokenService
       )
    {
        _userAccountService = userAccountService;
        _userContext = userContext;
        _emailService = emailService;
        _tenantSetter = tenantSetter;
        _tokenService = tokenService;
    }

    // Registration Flow 1: User accesses the registration page
    public IActionResult Registration ()
    {
        var objModel = new RegistrationViewModel();

        return View (objModel);
    }

    // Registration Flow 2: User submits the registration form.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registration (RegistrationViewModel registrationViewModel)
    {
        if ( ModelState.IsValid )
        {
            return View (registrationViewModel);
        }

        try
        {
            var that = this!;

            UserAccountDataModel userAccountDataModel
            = AuthExtensions.MapToDataModel(registrationViewModel != null ?
            registrationViewModel : new RegistrationViewModel());

            // Create the tenant user account (ApplicationUser)
            IdentityResult result =
            await _userAccountService.CreateApplicationUserAccount
            ( userAccountDataModel );

            string email =  registrationViewModel?.Email ?? string.Empty;

            if ( result.Succeeded )
            {
                await EmailExtensions.SendVerifyEmail
                (( IUrlHelper ) that,_userAccountService,
                _emailService,email,HttpContext);

                return RedirectToAction ("VerifyEmailSent");
            }

            return View (registrationViewModel);
        }
        catch
        {
            throw;
        }
    }

    // Registration Flow 3: User requested to check email
    public IActionResult VerifyEmailSent ()
    {
        ViewData["Title"] = "Email Sent";
        return View ();
    }

    // Registration Flow 4: User clicks Verification link
    public async Task<IActionResult> VerifyLink (string email,string token)
    {
        if ( string.IsNullOrEmpty (email) || string.IsNullOrEmpty (token) )
        {
            return BadRequest ("Invalid verification request parameters.");
        }

        _ = _userContext.GetCreateBaseDataModel ();
        _ = await _userAccountService.CompleteEmailVerification (email,token);

        return RedirectToAction ("VerifyComplete");
    }

    // Registration Flow 5: Tenant Account is confirmed.(Email verified)
    public IActionResult VerifyComplete ()
    {
        ViewData["Title"] = "Verification Complete";
        return View ();
    }

    // Login Flow 1: login page
    public IActionResult Login ()
    {
        LoginViewModel loginDisplayViewModel = new("Login");
        return View (loginDisplayViewModel);
    }

    // Login Flow: login form submit (1. authentication, 2. authorization (jwt token)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login (LoginViewModel loginDisplayViewModel)
    {
        var that = this!;
        string email =  loginDisplayViewModel?.Email ?? string.Empty;

        if ( ModelState.IsValid )
        {
            loginDisplayViewModel!.Message = "Invalid login attempt. Please check your credentials and try again.";
            return View ("Login",loginDisplayViewModel);
        }

        // (1. Authentication)
        Guid resolvedTenantId = _tenantSetter.CurrentTenantId;
        ApplicationUserDataModel? applicationIdentityUserDataModel
        = await _userAccountService.GetApplicationUser(email, resolvedTenantId!);

        // Validation: email verified, user exists (2. Authentication)
        if ( await AuthentiicationExtensions.InvalidApplicationUser (_userAccountService,applicationIdentityUserDataModel,
        loginDisplayViewModel!,resolvedTenantId) )
        {
            bool emailConfirm  = loginDisplayViewModel?.EmailConfirmed ?? true;
            if ( !emailConfirm )
            {
                await EmailExtensions.SendVerifyEmail
                (( IUrlHelper ) that,_userAccountService,
                _emailService,email,HttpContext);
            }

            // Can not login: validationfailed (2.Authentication)
            return View ("Login",loginDisplayViewModel);
        }

        // User login  (3. Authentication)
        bool result =
                await AuthentiicationExtensions.PasswordSignInAsync
                    ( _userAccountService,
                    applicationIdentityUserDataModel!.UserName!,
                    loginDisplayViewModel!.Password,
                    isPersistent: false,
                    lockoutOnFailure: false );

        // Login successful (4. Authentication, ended)
        if ( result )
        {

            // Get tenant specific role (1. Authhorization, start)
            string tenantRole =  await AuthorizationExtensions.GetTenantUserRole(_userAccountService, email, resolvedTenantId);

            // Auth Jwt Token (5. Authentication)
            AuthorizationExtensions.AddTenantIsolatedHeaderToken
            (HttpContext,_tokenService,applicationIdentityUserDataModel.Id,
            resolvedTenantId,tenantRole.ToString (),15,7);

            // Formated tenant role  (3. Authhorization)
            string formatedTenantRole =
            $"{applicationIdentityUserDataModel.Id}:{resolvedTenantId}:{tenantRole
            .ToString ()}";

            // HttpContext Responce UserClaims (4. Authhorization)
            AuthorizationExtensions.AddUserClaims
            (HttpContext,applicationIdentityUserDataModel.Id,
            resolvedTenantId,formatedTenantRole,
            applicationIdentityUserDataModel!.UserName!,
            applicationIdentityUserDataModel?.Email!);

            // Successful login, redirect to home page or dashboard
            return RedirectToAction ("Index","Home");
        }

        // Do not reveal if the password is incorrect or the account is locked, show a generic error message
        return View (loginDisplayViewModel);
    }


    // Logout Flow: User clicks the logout button, which triggers the Logout action that signs the user out and redirects to the home page
    public async Task<IActionResult> Logout ()
    {
        await _userAccountService.SignOutAsync ();

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var tenantId = _tenantSetter.CurrentTenantId;

        // 1. Invalidate long-lived token on the backend server database
        if ( !string.IsNullOrEmpty (userId) )
        {
            _ = await _tokenService.RevokeUserRefreshTokensAsync (userId,tenantId);
        }

        // 2. Erase both token cookies from the browser
        Response.Cookies.Delete ($".App.AccessToken.{tenantId}",new CookieOptions { Path = "/" });
        Response.Cookies.Delete ($".App.RefreshToken.{tenantId}",new CookieOptions { Path = "/account/refresh-token" });

        // 3. Clear your custom tenant session state and default antiforgery structures
        Response.Cookies.Delete ($".AspNetCore.Antiforgery.{tenantId}",new CookieOptions { Path = "/" });
        HttpContext.Session.Clear ();

        // 4. CLIENT-SIDE: Signal modern browsers to wipe all local origins data
        // Clears local storage, session storage, and HTTP cache
        Response.Headers.Append ("Clear-Site-Data","\"cache\", \"storage\"");

        // 5. CLIENT-SIDE: Instruct proxy (Nginx) and browser to never cache this response
        Response.Headers.Append ("Cache-Control","no-cache, no-store, must-revalidate");
        Response.Headers.Append ("Pragma","no-cache");
        Response.Headers.Append ("Expires","0");

        // 6. CLIENT-SIDE: Explicitly wipe cookies via expiration headers
        // Deletes the dynamically suffixed multi-tenant antiforgery cookie
        var antiforgeryCookieName = $".AspNetCore.Antiforgery.{tenantId}";
        Response.Cookies.Delete (antiforgeryCookieName,new CookieOptions
        {
            Path = "/",
            Secure = true,
            HttpOnly = true
        });

        // 7. Deletes standard ASP.NET Identity and Session cookies if they exist
        Response.Cookies.Delete (".AspNetCore.Identity.Application",new CookieOptions { Path = "/" });
        Response.Cookies.Delete (".AspNetCore.Session",new CookieOptions { Path = "/" });

        // 8. Redirect to login
        return RedirectToAction ("Login","Account");
    }



    // Password Reset Flow (1): User initiates password reset by providing email address 
    [HttpGet]
    public IActionResult ResetEmail ()
    {
        ViewData["Title"] = "Password Reset";

        return View (new ForgotPasswordViewModel ());
    }



    // Password Reset Flow (2): User submits email address to receive password reset link.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetEmail (ForgotPasswordViewModel forgotPasswordViewModel)
    {
        if ( !ModelState.IsValid )
        {
            return View (forgotPasswordViewModel);
        }

        var that = this!;
        var user = await _userAccountService.FindByEmailAsync(forgotPasswordViewModel.Email);

        if ( user != null &&
        await EmailExtensions.IsEmailConfirmed (_userAccountService,user?.Email!) )
        {
            await EmailExtensions.SendVerifyEmail
            (( IUrlHelper ) that,_userAccountService,
            _emailService,forgotPasswordViewModel.Email,HttpContext);

            return RedirectToAction ("SendVerifyEmail");
        }

        var result = await EmailExtensions.SendResetEmail
        (( IUrlHelper ) that, _userAccountService, _emailService, forgotPasswordViewModel.Email, HttpContext);

        return RedirectToAction (nameof (ResetEmailSent));
    }



    // Password Reset Flow (3): User is informed that reset email is sent.
    [HttpGet]
    public IActionResult ResetEmailSent ()
    {
        ViewData["Title"] = "Reset Email Sent";
        return View ();
    }


    // Password Reset Flow (4): User clicks the password reset link
    public async Task<IActionResult> ResetLink (string email,string token)
    {
        if ( string.IsNullOrEmpty (email) || string.IsNullOrEmpty (token) )
        {
            return BadRequest ("Invalid link request.");
        }

        var user = await _userAccountService.FindByEmailAsync(email);

        if ( user == null )
        {
            return BadRequest ("Invalid link request.");
        }

        var resetPasswordViewModel = new ResetPasswordViewModel()
        {
            Email = email,
            Token = token
        };

        return View ("ResetPassword",resetPasswordViewModel);
    }


    // Password Reset Flow - (5): User submits the new password
    [HttpPost]
    public async Task<IActionResult> ResetPassword (ResetPasswordViewModel resetPasswordViewModel)
    {
        if ( !ModelState.IsValid )
        {
            return View (resetPasswordViewModel);
        }

        ApplicationUserDataModel? applicationUserDataModel = await _userAccountService.FindByEmailAsync(resetPasswordViewModel.Email);


        // Reset with new password and invalidate the token and timestamp to prevent reuse 
        var email = applicationUserDataModel?.Email;

        bool result = await _userAccountService.ResetPasswordAsync(email!, resetPasswordViewModel.Token, resetPasswordViewModel.ConfirmPassword);

        if ( result )
        {
            return RedirectToAction (nameof (ResetComplete));
        }

        return View (resetPasswordViewModel);
    }


    // Password Reset Flow - (6): User is shown a confirmation page
    [HttpGet]
    public IActionResult ResetComplete ()
    {
        ViewData["Title"] = "Password Updated";
        return View ();
    }

    // Change Password Flow - (1): Authenticated user accesses the change password form
    [HttpGet]
    public async Task<IActionResult> ChangePassword ()
    {
        if ( User == null )
        {
            return RedirectToAction ("Login","Auth");
        }

        var changePasswordViewModel = new ChangePasswordViewModel
        {
            Email = User.Claims
            .FirstOrDefault (c => c.Type == ClaimTypes.Email)?.Value ?? ""
        };

        return View (changePasswordViewModel);
    }



    // Change Password Flow - (2): User submits the change password form 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword (ChangePasswordViewModel changePasswordViewModel)
    {

        if ( !ModelState.IsValid )
        {
            return View (changePasswordViewModel);
        }

        ApplicationUserDataModel? userIdentity = await _userAccountService.FindByEmailAsync(changePasswordViewModel.Email);

        if ( userIdentity == null )
        {
            return View (changePasswordViewModel);
        }

        var result =
        await _userAccountService.ChangePasswordAsync(userIdentity?.Email!, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

        if ( result )
        {
            return RedirectToAction (nameof (ResetComplete));
        }

        return View (changePasswordViewModel);
    }
}
