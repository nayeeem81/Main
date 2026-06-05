namespace DataTransferModel;

public class VerifyDataModel
{
    public VerifyDataModel ( )                          
    {
        Subject = "Please, first Verify Your Email to Login.";
    }

    public string UserName { get; set; } 


    public string Email { get; set; }


    public string Subject
    {
        get; set;
    }   


    public string Token { get; set; }


    public string Message { get; set; }


    public string VerifyLink
    {
        get;
        set;
    }
}
