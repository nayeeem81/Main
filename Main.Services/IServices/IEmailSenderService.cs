using DataTransferModel;

namespace Main.Services;

public interface IEmailSenderService
{
    Task<string> SendEmailAsync ( string userId );

    Task SendEmailVerificationAsync ( VerifyEmailDataModel verifyEmailDataModel);
}
