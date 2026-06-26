using DataTransferModel;
using System.Text.Json;
using WebAppCore.ViewModel;

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


    protected void SetSessionImageFile( ImageFile imageFile)
    {
        List<ImageFile>? listImageFile = SessionExtensions.GetObject<List<ImageFile>?>
                                                (HttpContext.Session, "ImageFileList");

        if ( listImageFile == null )
        {
            List<ImageFile>? listNewImageFile = new List<ImageFile>();
            imageFile.FileID = 1;
            listNewImageFile.Add ( imageFile );

            SessionExtensions.SetObject<List<ImageFile>>
               ( HttpContext.Session,"ImageFileList",listNewImageFile );
        }
        else
        {
            listImageFile = listImageFile.OrderBy( a => a.FileID ).ToList();
            int currentId = listImageFile.Last ( ).FileID;
            currentId += 1;

            imageFile.FileID = currentId;

            listImageFile.Add( imageFile );

            SessionExtensions.SetObject<List<ImageFile>>
               ( HttpContext.Session,"ImageFileList",listImageFile );
        }
    }

    protected List<ImageFile> GetAllSessionImages()
    {
        List<ImageFile>? listImageFiles = SessionExtensions.GetObject<List<ImageFile>?>(
            HttpContext.Session, "ImageFileList");
        
        if (listImageFiles != null)
        {
            return listImageFiles.ToList();
        }
            
        return new List<ImageFile> ();
    }

    protected bool RemoveSessionImageFile(int iageFileId)
    {
        List<ImageFile>? listImageFiles = SessionExtensions.GetObject<List<ImageFile>?>
                                (HttpContext.Session, "ImageFileList");

        if ( listImageFiles == null )
        {
            return false;
        }
            
        ImageFile? imageFile = listImageFiles.Where(a => a.FileID == iageFileId)
                                             .FirstOrDefault();

        if ( imageFile == null )
        {
            return false;
        }
        
        listImageFiles.Remove(imageFile);

        SessionExtensions.SetObject<List<ImageFile>?>
            (HttpContext.Session, "ImageFileList", listImageFiles);

        return true;
        
    }

    protected void ClearImageFileListSession ( )
    {
        HttpContext.Session.Remove( "ImageFileList" );
    }  
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
