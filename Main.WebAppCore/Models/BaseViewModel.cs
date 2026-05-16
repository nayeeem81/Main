using Main.Common;
using Main.Common.EnumClasses;
using Main.Common.Model;

namespace WebApp.ViewModel;

public class BaseViewModel
{
        public BaseViewModel()
        {
            Currency = StaticAppSettings.Currency;
        }

        public string? PageName { get; set; } = string.Empty;

        public EnumCurrency? Currency { get; set; }
       
        public EnumCompanyName? HostCompanyName { get; set; }
       
        public EnumCountry? HostCountry { get; set; }

        public ModelBase? ModelBase { get; set; }

        public void SetModelBase(ModelBase modelBase)
        {
            ModelBase = new ModelBase();
            ModelBase = modelBase;
        }    
}
