using DataTransferModel;
using Main.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebApp.ViewModel;

[Authorize ( Roles = "Admin" )]
public class AdminController: Controller
{
    private readonly IAccountService _accountService;
    private readonly IEmailSenderService _emailSenderService; 


    public AdminController ( IAccountService accountService, 
        IEmailSenderService emailSenderService )
    {
        _accountService = accountService;

        _emailSenderService = emailSenderService;
    }

    [HttpGet]
    public async Task<IActionResult> UserDashboard ( )
    {
        List<IdentityUserDataModel>? listIdentityUserDataModel = await _accountService.Users ( );

        List<IdentityUserDisplayViewModel> listIdentityUserDisplayViewModels 
            = new List<IdentityUserDisplayViewModel>();

        IdentityUserDisplayViewModel identityUserDisplayViewModel;

        listIdentityUserDataModel?.ForEach ( identityUserDataModel =>
        {
            identityUserDisplayViewModel = new IdentityUserDisplayViewModel
            {
                UserId = identityUserDataModel.UserId,
                UserName = identityUserDataModel.UserName,
                LockoutEnd = identityUserDataModel.LockoutEnd
            };

            listIdentityUserDisplayViewModels.Add( identityUserDisplayViewModel );
        } );

        return View ( listIdentityUserDisplayViewModels );
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnlockUser ( string userId )
    {
        bool success = await _accountService.UnlockUser ( userId );

        if (!success)
        {
            TempData["ErrorMessage"] = $"Failed to unlock account for user with ID {userId}.";
        }

        string userName = await _emailSenderService.SendEmailAsync ( userId );

        TempData["SuccessMessage"] = $"Unlocked and notified {userName}.";

        return RedirectToAction ( nameof ( UserDashboard ) );
    }
}
