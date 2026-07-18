namespace Main.WebAppCore.DependentServices;

public static class AppSettings
{
    public static MyConfigSettings Current
    {
        get; set;
    } = new ();
}
