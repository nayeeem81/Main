using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using ResourceLibrary;
using System.Security.Claims;
using WebApp.Infrastructure;
using WebApp.ViewModel;
using Main.Common.HelperRelated;
using Main.Services;

namespace FineArtsWebApp;

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
        var objModel = new AccountViewModel();
        objModel.PageName = "Registration Page";
               return View(objModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignUp(AccountViewModel model)
    {
        UserAccountDataModel modelData =            MapToDataModel(model);

        bool result 
            = await _userAccountService.CreateUserAccount ( modelData );

        return RedirectToAction("Login");    
    }

    private UserAccountDataModel MapToDataModel ( AccountViewModel model )
    {
        UserAccountDataModel modelData
            = new UserAccountDataModel();

        modelData.Email = model.Email;
        modelData.PhoneNumber = model.Phone;
        modelData.UserName
          = StringRelated
            .GetUserNameFromEmail ( model.Email );

        modelData.NormalizedUserName
            = model.Email.ToUpper ( );

        modelData.Password = model.Password;

        return modelData;
    }

    public IActionResult Login()
    {
        var objModel = new AccountViewModel();
        objModel.PageName = "Login";

        return View(objModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountViewModel model)
    {
        var userIdentity = 
            await _userManager
                  .FindByEmailAsync(model.Email);

        if (userIdentity == null)
        {
            return BadRequest("Invalid credentials");
        }

        var resultSignIn = await 
                        _signInManager
                        .PasswordSignInAsync(
                                userIdentity, 
                                model.Password, 
                                true, 
                                lockoutOnFailure: false);

        if (resultSignIn.Succeeded)
        {
            int userID = await _userAccountService.GetSingleUser(userIdentity.Id);

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
        await _signInManager.SignOutAsync();
       
        ClearSessionUser();

        ClearModelBaseSession();

        return RedirectToAction("Index", "Home");
    }


    [Authorize(Roles = "Admin,User,Company")]
    public ActionResult ResetPassword()
    {
        if (User.Identity.IsAuthenticated)
        {
            var objModel = new AccountViewModel() 
            { 
                PageName = "Reset Password" 
            };

            return View(objModel);
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpPost]
    [Authorize(Roles = "Admin,User,Company")]
    public async Task<ActionResult> ResetPassword(
        AccountViewModel accountViewModel)  
    {
        var isValid = ValidationService
            .IsValidEmail(accountViewModel.Email);
        
        _logger.LogInformation("Password reset attempt for email: {Email}, Valid Email: {IsValid}", accountViewModel.Email, isValid);
        
        if(!isValid)
        {
            _logger.LogWarning("Invalid email format for password reset: {Email}", accountViewModel.Email);
            return RedirectToAction("ResetPassword");
        }

        var user = await _userManager.FindByEmailAsync(accountViewModel.Email);

        if (user == null)
        {
            _logger.LogWarning("Password reset attempt failed for email: {Email}", accountViewModel.Email);
            return RedirectToAction("ResetPassword");
        }

        try
        {
            var result = await _userManager.ChangePasswordAsync(user, accountViewModel.CurrentPassword, accountViewModel.NewPassword);
            
            if (result.Succeeded)
            {
                // Optional: Refresh sign-in to update cookies/claims
                return RedirectToAction("Login");
            }

            return RedirectToAction("ResetPassword");
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            _logger.LogError(ex, "Error sending password reset email to {Email}", accountViewModel.Email);
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
