namespace Main.WebAppCore.ViewModels;

public class InviteUserViewModel
{
    public string Email { get; set; } = string.Empty;
    public string TenantRole { get; set; } = "ContentManager";
}

public class AcceptInvitationViewModel
{
    public string Token { get; set; } = string.Empty;
    public string? FullName { get; set; }
}
