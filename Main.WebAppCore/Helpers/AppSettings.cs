using WebAppCore.Helper;
public static class AppSettings
{
    public static MyConfigSettings Current
    {
        get; set;
    } = new ();
}
