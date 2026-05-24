using Main.Common.Enums;

namespace WebApp.ViewModel;

public class BaseViewModel
{
        public BaseViewModel()
        {
        }

        public string? PageName { get; set; } = string.Empty;

        public EnumCurrency? Currency { get; set; }
       
        public EnumCompanyName? HostCompanyName { get; set; }
       
        public EnumCountry? HostCountry { get; set; }

}
