using System.ComponentModel.DataAnnotations;
using Main.Common;

namespace Main.Model
{        
    public class User : BaseEntity
    {                
        public User() {
        }

        //Auth User() Create (first sign up time using Identity) //Visitor User
        public User(string identityUserID,
                    string email, string clientName,
                    EnumCountry country, EnumCompanyName company)
        {
            IdentityUserID = identityUserID;
            Email = email;
            ClientName = clientName;
            HostCompanyName = company;
            HostCountry = country;

            CreatedBy = 1; //Admin (Hard Coded Seed Data)
            ModifiedBy = 1; //Admin  (Hard Coded Seed Data)
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        //Seed Data Constructor
        public User(string identityUserID, 
                    string email,
                    string clientName)
        {
            IdentityUserID = identityUserID;
            Email = email;
            ClientName = clientName;
            
            ModifiedBy = 1;
            CreatedBy = 1;
            CreatedDate = DateTime.MinValue;
            ModifiedDate = DateTime.MinValue;

            HostCountry = EnumCountry.Bangladesh;
            HostCompanyName = EnumCompanyName.FineArts;
            IsActive = true;
        }
        
        
        [Key]
        public Int32 UserID { get; set; }

        [Required]
        public string IdentityUserID { get; set; }

        public EnumUserAccountType? UserAccountType { get; set; }

        public string ClientName { get; set; }

        public string Email { get; set; }

        public string? Website { get; set; }

        public string? Phone { get; set; }

        public string? Remarks { get; set; }

        public double? AccountBalance { get; set; }
    }
}
