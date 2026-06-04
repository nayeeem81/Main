using DataTransferModel;

namespace Main.Services;

public interface IEmailSenderService
{
    Task<string> SendEmailAsync ( string userId );

    Task SendEmailAsync ( string email,string subject,string htmlMessage );

    Task SendEmailVerificationAsync ( VerifyDataModel verifyEmailDataModel);

    Task SendResetPasswordEmailAsync ( ResetDataModel resetEmailDataModel );
}
