namespace Main.Common;

public class TokenResponseModel
{
    public TokenResponseModel (bool valid)
    {
        IsSuccess = valid;
    }

    public bool IsSuccess
    {
        get; set;
    }

    public string AccessToken
    {
        get; set;
    }

    public string RefreshToken
    {
        get; set;
    }
}
