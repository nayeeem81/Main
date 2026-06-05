using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using FluentEmail.Core;
using DataTransferModel;

namespace Main.Services;

public class EmailSenderService: IEmailSender, IEmailSenderService
{
    private readonly IFluentEmailFactory _emailFactory;

    private readonly UserManager<IdentityUser> _userManager;
    

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



    public async Task SendEmailAsync ( string email, string subject, string htmlMessage )
    {
        var response = await _emailFactory
                .Create()
                .To(email)
                .Subject(subject)
                .Body(htmlMessage, isHtml: true)
                .SendAsync();

        if ( !response.Successful )
        {
            throw new Exception ( $"Email delivery failed: {string.Join ( ", ",response.ErrorMessages )}" );
        }
    }



    public async Task SendEmailVerificationAsync ( VerifyDataModel verifyEmailDataModel )
    {
        string template 
            = @"
            <html>
            <body>
                <p>Hi {{ Name }},</p>
                <p>Please click the link below to verify your email:</p>
                <p>
                    <a href='{{ LinkUrl }}' style='color: #007bff; text-decoration: underline;'>
                        Verify Email
                    </a>
                </p>
            </body>
            </html>";

        string populatedTemplate = template
                    .Replace("{{ Name }}", verifyEmailDataModel.UserName)
                    .Replace("{{ LinkUrl }}", verifyEmailDataModel.VerifyLink ?? string.Empty);

        await SendEmailAsync ( 
            verifyEmailDataModel.Email,
            verifyEmailDataModel.Subject,
            populatedTemplate );
    }


    public async Task SendResetPasswordEmailAsync ( ResetDataModel resetEmailDataModel )
    {
        string template
            = @"
            <html>
            <body>
                <p>Hi {{ Name }},</p>
                <p>Please click the link below to reset your password:</p>
                <p>
                    <a href='{{ LinkUrl }}' style='color: #007bff; text-decoration: underline;'>
                        Reset Password  
                    </a>
                </p>
            </body>
            </html>";

        string populatedTemplate = template
                    .Replace("{{ Name }}", resetEmailDataModel.UserName)
                    .Replace("{{ LinkUrl }}", resetEmailDataModel.ResetLink ?? string.Empty);

        await SendEmailAsync (
            resetEmailDataModel.Email,
            resetEmailDataModel.Subject,
            populatedTemplate );
    }
}
