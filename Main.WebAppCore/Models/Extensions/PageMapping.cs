using DataTransferModel;
using Main.Common.Enums;

namespace WebAppCore.ViewModel.Extensions;

public static class PageMapping
{
    public static List<PageDisplayViewModel> PageDisplayMapping ( List<PageDisplayDataModel> listPageDataModel )
    {
        List<PageDisplayViewModel> listPageDisplayViewModels = new List<PageDisplayViewModel> ();

        PageDisplayViewModel pageDisplayViewModel;

        listPageDataModel.ForEach( dataModel  =>
        {
            pageDisplayViewModel = new PageDisplayViewModel ();

            pageDisplayViewModel.PageName = ListEnum.GetPageDescription ( dataModel.EnumPublicPage );

            pageDisplayViewModel.CompanyName = 
                                            ListEnum.GetCompanyDescription ( dataModel.EnumCompanyName );

            listPageDisplayViewModels.Add ( pageDisplayViewModel );
        }  );

        return listPageDisplayViewModels;
    }

}