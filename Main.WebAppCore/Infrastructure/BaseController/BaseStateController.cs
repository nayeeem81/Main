using DataTransferModel;
using FineArtsWebApp;
using System.Text.Json;
using WebApp.ViewModel;

namespace Main.WebAppCore;

public partial class BaseController
{
    protected void SetSessionRechargeSubCategoryID(int? subCategoryID)
    {
        HttpContext.Session.SetInt32("SubCategoryID", subCategoryID.HasValue ? subCategoryID.Value : -1);
    }
    
    protected int GetSessionRechargeSubCategoryID()

    {
        var SubCatID = HttpContext.Session.GetInt32("SubCategoryID");
        return SubCatID.HasValue ? SubCatID.Value : -1;
    }

    protected void ClearSessionRechargeSubCategoryID()
    {
        HttpContext.Session.Remove("SubCategoryID");
    }
   
    #region search model
    protected void SetSessionSearchModel(SearchModel searchModel)
    {
        SessionExtensions.SetObject<SearchModel>(HttpContext.Session, "search", searchModel);
    }

    protected SearchModel? GetSessionSearchModel()
    {
        var objSessionSearchModel = SessionExtensions.GetObject<SearchModel>(HttpContext.Session, "search");
        if (objSessionSearchModel != null)
            return (SearchModel)objSessionSearchModel;
        return null;
    }

    protected void ClearSessionSearchModel()
    {
        HttpContext.Session.Remove("search");
    }

    #endregion 

    #region Sarcch Image Session
    protected void SetSearchResultListPostVM(List<ProductViewModel> listPostVM)
    {
        SessionExtensions.SetObject<List<ProductViewModel>>(HttpContext.Session, "SearchResultListPostVM", listPostVM);
    }

    protected List<ProductViewModel> GetSearchResultListPostVM()
    {
        var result = SessionExtensions.GetObject<List<ProductViewModel>>(HttpContext.Session, "SearchResultListPostVM");

        if (result != null)
        {
            return (List<ProductViewModel>)result;
        }
        return new List<ProductViewModel>();
    }

    protected void ClearSearchResultListPostVM()
    {
        HttpContext.Session.Remove("SearchResultListPostVM");
    }

    protected void SetSearchPostViewModel( ProductViewModel searchModel )
    {
        SessionExtensions.SetObject<ProductViewModel>(HttpContext.Session, "searchpostvm", searchModel);
    }

    protected ProductViewModel? GetSearchPostViewModel ()
    {
        var objSearchPostViewModel = SessionExtensions.GetObject<ProductViewModel>(HttpContext.Session, "searchpostvm");

        if (objSearchPostViewModel != null)
            return (ProductViewModel )objSearchPostViewModel;
        return null;
    }

    protected void ClearSearchPostViewModel()
    {
        HttpContext.Session.Remove("searchpostvm");
    }
    #endregion

    #region Product Image Session
    protected void SetSessionNewProductImage(ProductFileViewModel file)
    {
        var list = SessionExtensions.GetObject<List<ProductFileViewModel>>(HttpContext.Session, "NewProductImageList");
        if (list != null)
        {
            int count = list.Count;
            count = count + 1;
            file.ProductImageFileID = count;

            list.Add(file);
            SessionExtensions.SetObject<List<ProductFileViewModel>>(HttpContext.Session, "NewProductImageList", list);
        }
        else
        {
            file.ProductImageFileID = 1;
            List<ProductFileViewModel> objListFiles = new List<ProductFileViewModel>();
            objListFiles.Add(file);
            SessionExtensions.SetObject<List<ProductFileViewModel>>(HttpContext.Session, "NewProductImageList", objListFiles);
        }
    }

    protected List<ProductFileViewModel> GetSessionNewProductImage()
    {
        var list = SessionExtensions.GetObject<List<ProductFileViewModel>>(HttpContext.Session, "NewProductImageList");
        if (list != null)
        {
            return list.ToList();
        }
        else
        {
            return new List<ProductFileViewModel>();
        }
    }

    protected bool RemoveSessionNewProductImage(int id)
    {
        var list = SessionExtensions.GetObject<List<ProductFileViewModel>>(HttpContext.Session, "NewProductImageList");
        if (list != null)
        {
            var obj = list.Where(a => a.ProductImageFileID == id).FirstOrDefault();
            if (obj != null)
            {
                list.Remove(obj);
                SessionExtensions.SetObject<List<ProductFileViewModel>>(HttpContext.Session, "NewProductImageList", list);
                return true;
            }
        }
        return false;
    }

    protected void ClearNewProductImageSessions()
    {
        HttpContext.Session.Remove("NewProductImageList");
    }

    #endregion

    #region Admin Post Image Session
    protected void SetSessionNewAdminPostImage( AdminImageFileDataModel file )
    {
        var list = SessionExtensions.GetObject<List<AdminImageFileDataModel>>(HttpContext.Session, "NewAdminPostImageList");
        if (list != null)
        {
            int count = list.Count;
            count = count + 1;
            file.AdminImageFileID = count;
            list.Add(file);
            SessionExtensions.SetObject<List<AdminImageFileDataModel>>(HttpContext.Session, "NewAdminPostImageList", list);
        }
        else
        {
            file.AdminImageFileID = 1;
            List<AdminImageFileDataModel> objListFiles = new List<AdminImageFileDataModel>();
            objListFiles.Add(file);
            SessionExtensions.SetObject<List<AdminImageFileDataModel>>(HttpContext.Session, "NewAdminPostImageList", objListFiles);
        }
    }

    protected List<AdminImageFileDataModel> GetSessionNewAdminPostImage()
    {
        var list = SessionExtensions.GetObject<List<AdminImageFileDataModel>>(HttpContext.Session, "NewAdminPostImageList");
        if (list != null)
        {
            return list.ToList();
        }
        else
        {
            return new List<AdminImageFileDataModel>();
        }
    }

    protected bool RemoveSessionNewAdminPostImage(long id)
    {
        var list = SessionExtensions.GetObject<List<AdminImageFileDataModel>>(HttpContext.Session, "NewAdminPostImageList");
        if (list != null)
        {
            var obj = list.Where(a => a.AdminImageFileID == id).FirstOrDefault();
            if (obj != null)
            {
                list.Remove(obj);
                SessionExtensions.SetObject<List<AdminImageFileDataModel>>(HttpContext.Session, "NewAdminPostImageList", list);
                return true;
            }
        }
        return false;
    }

    protected void ClearNewAdminPostImageSessions()
    {
        HttpContext.Session.Remove("NewAdminPostImageList");
    }
    #endregion

}


public static class SessionExtensions
{
    public static void SetObject<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}
