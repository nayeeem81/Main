namespace DataTransferModel;

public class VerifyEmailDataModel
{
    public VerifyEmailDataModel ( )                          
    {
    }


    public VerifyEmailDataModel ( string email, string code )
    {
        Email = email;
        Code = code;
    }

    public string UserName { get; set; } 


    public string Email { get; set; }


    public string Subject
    {
        get; set;
    }   


    public string Code { get; set; }


    public string Message { get; set; }


    public string? VerifyLink
    {
        get; set;
    }

}
