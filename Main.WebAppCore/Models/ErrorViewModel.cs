namespace WebAppCore.ViewModel;

public class ErrorViewModel: BaseViewModel
{
    public ErrorViewModel ( )
    {
        PageName = "Error";
    }
   
    public string ErrorMesssgae { get; set; }
   
    public string ErrorCode { get; set; }

    public string StatusCode
    {
        get; set;
    }

    public int ErrorNumber
    {
        get; set;
    } 
}
