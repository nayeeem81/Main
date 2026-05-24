using DataTransferModel;

namespace Main.Services
{
    public class UserAccountDataModel : DataModel
    {
        public UserAccountDataModel() { }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber
        {
            get; set;
        }

        public string NormalizedUserName
        { get; set; }

        public string Password
        {
            get; set;
        }
    }
}
