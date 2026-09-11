using DataTransferModel;
using Main.Common;
using Main.Infrastructure;
using Main.Infrastructure.ICrosscuttingServices;
using Main.Services;
using Main.WebAppCore.Controllers.ControllerExtensions;
using Main.WebAppCore.Filters;
using Main.WebAppCore.Models;
using Main.WebAppCore.Models.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Main.WebAppCore.Controllers;

public class AuthController: BaseController
{
    private readonly ITenantSetter _tenantSetter;
    private readonly IAccountService _userAccountService;
    private readonly IEmailSenderService _emailService;
    private readonly ITokenService _tokenService;

    public AuthController (
        IAccountService userAccountService,
        IEmailSenderService emailService,
        ITenantSetter tenantSetter,
        ITokenService tokenService
       )
    {
        _userAccountService = userAccountService;
        _emailService = emailService;
        _tenantSetter = tenantSetter;
        _tokenService = tokenService;
    }

    // Registration Flow 1: User accesses the registration page
    [HttpGet]
    [TypeFilter (typeof (TransactionAttribute))]
    public IActionResult Registration ()
    {
        var objModel = new RegistrationViewModel();

        return View (objModel);
    }

    // Registration Flow 2: User submits the registration form.
    [HttpPost]
    [TypeFilter (typeof (TransactionAttribute))]
    public async Task<IActionResult> Registration (RegistrationViewModel registrationViewModel)
    {
        if ( !ModelState.IsValid )
        {
            return View (registrationViewModel);
        }

        _ = this!;

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
            await SendVerifyEmail (email,HttpContext);

            return RedirectToAction ("VerifyEmailSent");
        }

        return View (registrationViewModel);

    }

    public async Task SendVerifyEmail
    (string? email,HttpContext context)
    {
        string localEmail = email ??  string.Empty;
        string emailVerifyToken = await _userAccountService.GetEmailVerifyToken (localEmail);

        string? verifyLink = Url.Action (
            action: "VerifyLink",
            controller: "Auth",
            values: new
            {
                Email = email, Token = emailVerifyToken
            },
            protocol: Request.Scheme
        );

        var verifyEmailDataModel = new VerifyDataModel ()
        {
            Email = localEmail , VerifyLink = verifyLink!
        };

        await _emailService.SendEmailVerificationAsync (verifyEmailDataModel);
    }


    public IActionResult VerifyEmailSent ()
    {
        ViewData["Title"] = "Email Sent";
        return View ();
    }


    public async Task<IActionResult> VerifyLink (string email,string token)
    {
        if ( string.IsNullOrEmpty (email) || string.IsNullOrEmpty (token) )
        {
            return BadRequest ("Invalid verification request parameters.");
        }

        _ = await _userAccountService.CompleteEmailVerification (email,token);

        return RedirectToAction ("VerifyComplete");
    }


    public IActionResult VerifyComplete ()
    {
        ViewData["Title"] = "Verification Complete";
        return View ();
    }


    public IActionResult Login ()
    {
        LoginViewModel loginDisplayViewModel = new("Login");
        return View (loginDisplayViewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Login (LoginViewModel loginDisplayViewModel)
    {
        if ( loginDisplayViewModel == null )
        {
            ModelState.AddModelError (string.Empty,"Invalid login attempt form payload.");
            return View (new LoginViewModel ());
        }

        string email = loginDisplayViewModel?.Email ?? string.Empty;

        if ( !ModelState.IsValid )
        {
            return View ("Login",loginDisplayViewModel);
        }

        ApplicationUserDataModel? applicationUser = await _userAccountService.GetApplicationUser (email);

        bool result = await IsEmailConfirmed (email);
        if ( !result )
        {
            await SendVerifyEmail (email,HttpContext);
            return View (new LoginViewModel ());
        }

        // This method is totally safe! It only checks the password hashes without ghost cookies.
        bool signinresult = await _userAccountService.PasswordSignInAsync (applicationUser?.Email!,loginDisplayViewModel?.Password!);

        if ( signinresult )
        {
            string tenantRole = await _userAccountService.GetTenantUserRoleClaim(email, _tenantSetter.ResolvedTenantId);
            string formatedTenantRole = $"{applicationUser?.Id ?? ""}:{_tenantSetter.ResolvedTenantId }:{tenantRole}";

            // Safely declare your token timing definitions
            int accessJwtMinutes = 15;
            int maxRefreshDays = 7;

            var accessJwt = await _tokenService.GenerateAccessToken(applicationUser?.Id!, _tenantSetter.ResolvedTenantId, formatedTenantRole, tenantRole, applicationUser?.UserName!, email, accessJwtMinutes);
            var refreshTokenStr = _tokenService.GenerateRefreshToken();

            _ = await _tokenService.SaveRefreshToken (applicationUser?.Id!,_tenantSetter.ResolvedTenantId,refreshTokenStr);

            var baseCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/"  ,
                Domain = HttpContext.Request.Host.Host
            };

            //  FIX: Let the browser store the access cookie file for 7 days.
            // Your native .AddJwtBearer middleware validation parameters (ValidateLifetime = true) 
            // will strictly handle checking and expiring the token after 15 minutes.
            HttpContext.Response.Cookies.Append ($".App.AccessToken.{_tenantSetter.ResolvedTenantId}",accessJwt,
                new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (maxRefreshDays) });

            HttpContext.Response.Cookies.Append ($".App.RefreshToken.{_tenantSetter.ResolvedTenantId}",refreshTokenStr,
                new CookieOptions (baseCookieOptions) { Expires = DateTimeOffset.UtcNow.AddDays (maxRefreshDays) });

            return RedirectToAction ("Index","Home",new
            {
                area = ""
            });
        }

        ModelState.AddModelError (string.Empty,"Invalid login attempt.");
        return View ("Login",loginDisplayViewModel);
    }


    public async Task<bool> IsEmailConfirmed (string? email)
    {
        bool result = await _userAccountService.IsEmailConfirmedAsync (email ?? "");

        return result;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout ()
    {
        Guid tenantId = _tenantSetter.ResolvedTenantId;
        string userId = _tenantSetter.HttpContextUserId;


        var accessTokenName = $".App.AccessToken.{tenantId.ToString()}";
        string refreshTokenName = $".App.RefreshToken.{tenantId.ToString()}";


        _ = await _tokenService.RevokeUserRefreshTokensAsync (userId,_tenantSetter.ResolvedTenantId);


        var baseCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/",
            Domain = HttpContext.Request.Host.Host
        };


        HttpContext.Response.Cookies.Delete (accessTokenName,baseCookieOptions);
        HttpContext.Response.Cookies.Delete (refreshTokenName,baseCookieOptions);


        Response.Headers.Append ("Cache-Control","no-cache, no-store, must-revalidate");
        Response.Headers.Append ("X-Clear-Cache","true");

        return RedirectToAction ("Index","Home");
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
