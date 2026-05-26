using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Main.Services;

public class EmailSenderService: IEmailSenderService
{
    public readonly IEmailSender _emailSender;
    public readonly UserManager<IdentityUser> _userManager;

    public EmailSenderService ( 
        IEmailSender emailSender ,
        UserManager<IdentityUser> userManager )
    {
        _emailSender = emailSender;
        _userManager = userManager;
    }

    public async Task<string> SendEmailAsync ( string userId )
    {
        IdentityUser? identityUser = await _userManager.FindByIdAsync ( userId );

        await _emailSender.SendEmailAsync (
            identityUser?.Email != null ? identityUser.Email : "",
            "Account Unlocked",
            "Your account has been manually unlocked by an administrator. You may now log in."
        );

        return identityUser?.UserName ?? string.Empty;
    }
}
