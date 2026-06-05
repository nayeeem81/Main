using System.ComponentModel.DataAnnotations;
using ResourceLibrary.Resources;

namespace WebApp.ViewModel;

public class RegistrationViewModel: BaseViewModel
{
    public RegistrationViewModel ( )
    {
    }

    [Required ( ErrorMessageResourceName = "UserNameRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "UserName",Prompt = "UserNamePlaceholder",ResourceType = typeof ( SharedResource ) )]
    public string UserName { get; set; }


    [Required ( ErrorMessageResourceName = "EmailRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "Email",Prompt = "EmailPlaceholder",ResourceType = typeof ( SharedResource ) )]
    public string Email { get; set; }


    [Required ( ErrorMessageResourceName = "PhoneRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "Phone",Prompt = "PhonePlaceholder",ResourceType = typeof ( SharedResource ) )]
    public string Phone { get; set; }

    [Required ( ErrorMessageResourceName = "PasswordRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "Password",Prompt = "PasswordPlaceholder",ResourceType = typeof ( SharedResource ) )]
    [DataType ( DataType.Password )]
    public string Password { get; set; }


    [Required ( ErrorMessageResourceName = "RePasswordRequired",ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "RePassword",Prompt = "RePasswordPlaceholder",ResourceType = typeof ( SharedResource ) )]
    [DataType ( DataType.Password )]
    public string RePassword { get; set; }


    [Required ( ErrorMessageResourceName = "ClientNameRequired", ErrorMessageResourceType = typeof ( SharedResource ) )]
    [Display ( Name = "ClientName", Prompt = "ClientNamePlaceholder", ResourceType = typeof ( SharedResource ) )]
    public string ClientName { get; set; }

}
