using BusinessModel;

using Data;

using Entity.Model;

using IRepository;

using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductImageRepository : IProductImageRepository
{
    private readonly BussinessAppDbContext _context;

    public ProductImageRepository( BussinessAppDbContext context )
    {
        _context = context;
    }

    public async Task<List<PanelPostDataModel>> GetSelectProducts(EnumCompanyName company) 
    {  
        List<Product> list = await _context.Products
                                    .Where( a => a.HostCompanyName == company)
                                    .ToListAsync();

        if ( list == null )
        {
            return new List<PanelPostDataModel> ( );
        }

        List<PanelPostDataModel> listSelectPanelPostDM = new List<PanelPostDataModel>();

        PanelPostDataModel objDM;

        int id = 1;

        list.ForEach ( entity => {

            entity.ListImageFiles.ToList ( ).ForEach ( file =>
            {
                objDM = new PanelPostDataModel ( );

                objDM.CategoryID = entity.CategoryID;
                objDM.PanelPostID = id;
                objDM.RootID = entity.ProductID;
                objDM.EnumPostType = entity.PostType;
                objDM.Price = entity.Price;
                objDM.PostTitle = entity.ProductName;
                objDM.ImageFileContent = file.ImageFileContent;
                objDM.ImageFileID = file.ProductImageFileID;

                id += 1;
                listSelectPanelPostDM.Add ( objDM );
            } );

        } );

        return listSelectPanelPostDM.OrderBy ( a => a.CategoryID ).ToList ( );
    }
}

 