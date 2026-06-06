using System.ComponentModel.DataAnnotations;
using Main.Common.Enums;

namespace Domain.Model;

public class User : BaseEntity
{                
    public User() 
    {
    }

    public User(string identityUserID,
                string email, 
                string clientName,
                EnumCountry country, 
                EnumCompanyName company)
    {
        Email = email;
        ClientName = clientName;
        HostCompanyName = company;
        HostCountry = country;

        CreatedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1"; //Admin 
        ModifiedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1"; //Admin 
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;

        IdentityUserId = identityUserID; 
    }

    //Seed Data Constructor
    public User(string identityUserID, 
                string email,
                string clientName)
    {
        Email = email;
        ClientName = clientName;
        
        ModifiedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedBy = "e02fd0e4-00fd-000a-ca30-0F00a0898ba1";
        CreatedDate = DateTime.MinValue;
        ModifiedDate = DateTime.MinValue;

        HostCountry = EnumCountry.Bangladesh;
        HostCompanyName = EnumCompanyName.FineArts;
        IsActive = true;

        IdentityUserId = identityUserID;
    }
    
    
    [Key]
    public int Id { get; set; }


    [Required]
    public EnumAccountType UserAccountType { get; set; }


    public string ClientName { get; set; }


    public string Email { get; set; }


    public string? Website { get; set; }


    public string? Phone { get; set; }


    public string? Remarks { get; set; }


    public double? AccountBalance { get; set; }
}
