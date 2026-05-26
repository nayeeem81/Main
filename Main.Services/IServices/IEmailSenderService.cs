namespace Main.Services;

public interface IEmailSenderService
{
    Task<string> SendEmailAsync ( string userId );
}
