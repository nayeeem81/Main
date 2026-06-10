using Main.Common;
using Main.Common.Enums;

namespace DataTransferModel
{
    public class PageDisplayDataModel 
    {
        public PageDisplayDataModel ( ) 
        {
        }

        public PageDisplayDataModel ( int id, EnumPublicPage enumPublicPage, 
            EnumCompanyName enumCompany)
        {
            PageID = id;
            EnumPublicPage = enumPublicPage;
            EnumCompanyName = enumCompany;
        }

        public int PageID { get; set; }

        public EnumPublicPage EnumPublicPage { get; set; }

        public EnumCompanyName EnumCompanyName { get; set; }
    }
}
