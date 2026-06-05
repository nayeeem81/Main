
namespace DataTransferModel;

public class ResetDataModel
{
    public ResetDataModel ( )                          
    {
        Subject = "Please, reset your password with this link.";
    }

    public string UserName
    {
        get; set;
    }

    public string Email { get; set; }


    public string Subject
    {
        get; set;
    }   


    public string Token { get; set; }


    public string Message { get; set; }


    public string ResetLink
    {
        get;
        set;
    }
}
