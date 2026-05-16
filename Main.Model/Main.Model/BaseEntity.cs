using Main.Common;
using Main.Common.EnumClasses;
using Main.Common.Model;

namespace Main.Model
{
    public class BaseEntity
    {        
        public BaseEntity() {            
            IsActive = true;
        } 
        
        public void CreateBaseData(ModelBase modelBase)
        {
            CreatedDate = modelBase.CreatedDate;
            CreatedBy = modelBase.CreatedBy;

            HostCompanyName = modelBase.HostCompanyName;
            HostCountry = modelBase.Country;

            ModifiedBy = modelBase.ModifiedBy;
            ModifiedDate = modelBase.ModifiedDate;

            IsActive = true;
        }

        public void ModifyBaseData(ModelBase modelBase)
        {
            ModifiedDate = modelBase.ModifiedDate;
            ModifiedBy = modelBase.ModifiedBy;

            HostCompanyName = modelBase.HostCompanyName;
            HostCountry = modelBase.Country;

            IsActive = true;
        }
       
        public int CreatedBy { get; set; }
       
        public DateTime CreatedDate { get; set; }
       
        public int ModifiedBy { get; set; }
       
        public DateTime ModifiedDate { get; set; }
      
        public EnumCompanyName HostCompanyName { get; set; }
       
        public EnumCountry HostCountry { get; set; }

        public bool IsActive { get; set; }
    }
}
