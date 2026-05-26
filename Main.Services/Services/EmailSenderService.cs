using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using FluentEmail.Core;

namespace Main.Services;

public class EmailSenderService: IEmailSender
{
    private readonly IFluentEmailFactory _emailFactory;
    public readonly UserManager<IdentityUser> _userManager;

    public EmailSenderService (  
        IFluentEmailFactory emailFactory,
        UserManager<IdentityUser> userManager )
    {
        _emailFactory = emailFactory;
        _userManager = userManager;
    }

    public async Task<string> SendEmailAsync ( string userId )
    {
        IdentityUser? identityUser = await _userManager.FindByIdAsync ( userId );

        await SendEmailAsync (
            identityUser?.Email != null ? identityUser.Email : "",
            "Account Unlocked",
            "Your account has been manually unlocked by an administrator. You may now log in."
        );

        return identityUser?.UserName ?? string.Empty;
    }

    public async Task SendEmailAsync ( string email,string subject,string htmlMessage )
    {
        var response = await _emailFactory
                .Create()
                .To(email)
                .Subject(subject)
                .Body(htmlMessage, isHtml: true)
                .SendAsync();

        if ( !response.Successful )
        {
            // Throw an exception or log the errors returned in response.ErrorMessages
            throw new Exception ( $"Email delivery failed: {string.Join ( ", ",response.ErrorMessages )}" );
        }
    }
}
