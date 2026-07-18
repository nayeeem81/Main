using Main.Common;
using Main.Infrastructure.CrosscuttingHelperServices;
using Main.Services;
using Microsoft.AspNetCore.Mvc;
using WebAppCore.ViewModel;

namespace Main.WebAppCore.Controllers.Extensions;

public static class EmailExtensions
{
    // Send verification link
    public static async Task SendVerifyEmail
    (IUrlHelper urlHelper,IAccountService accountService,
    IEmailSenderService emailService,string? email,HttpContext context)
    {
        string localEmail = email ??  string.Empty ;
        string emailVerifyToken = await accountService.GetEmailVerifyToken (localEmail);

        string verifyLink = UrlExtensions.GenerateUrlLink
        ( urlHelper, localEmail ,emailVerifyToken,"VerifyLink","Auth",context);

        var verifyEmailDataModel = new VerifyDataModel ()
        {
            Email = localEmail , VerifyLink = verifyLink
        };

        await emailService.SendEmailVerificationAsync (verifyEmailDataModel);
    }

    // User email confirmed?
    public static async Task<bool> IsEmailConfirmed (IAccountService accountService,LoginViewModel loginDisplayViewModel)
    {
        bool result = await accountService.IsEmailConfirmedAsync (loginDisplayViewModel.Email);

        return result;
    }

    public static async Task<bool> IsEmailConfirmed (IAccountService accountService,string email)
    {
        bool result = await accountService.IsEmailConfirmedAsync (email);

        return result;
    }


    public static async Task<bool> SendResetEmail
    (IUrlHelper urlHelper,IAccountService accountService,
    IEmailSenderService emailService,string email,HttpContext context)
    {
        var token = await accountService.GeneratePasswordResetTokenAsync(email);

        string resetLink = UrlExtensions.GenerateUrlLink
        (urlHelper, email , token , "ResetLink", "Auth", context);

        var resetDataModel = new ResetDataModel()
        {
            Email = email,
            ResetLink = resetLink
        };

        await emailService.SendResetPasswordEmailAsync (resetDataModel);

        return true;
    }
}




