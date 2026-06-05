namespace WebApp.ViewModel;

public class UserModel
{
    public UserModel() { }

    public UserModel(int userID)
    {
        UserID = userID;
    }

    public int UserID { get; set; }
}
