using Main.Common.Enums;
using Main.Common.Helper;
using Main.Common.Model;

namespace Main.Common.HelperServices;

public static class TenantStoreHelper
{
    public static List<TenantVariableModel> GetCategoryList ( EnumStoreType store )
    {
        List<TenantVariableModel>  listCategory = new  List<TenantVariableModel> ();

        listCategory =
            TenantStores.ListTenantStoreMenu ( )
            .Where<TenantVariableModel> ( m =>
            m.Variable == EnumTenantVariable.ProductCategory &&
            m.ValueID == m.ParentID &&
            m.TenantStore == store ).ToList ( );


        return listCategory.ToList ( );
    }

    public static List<TenantVariableModel> GetSubCategoryList ( EnumStoreType store )
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();

        listSubCategory =
            TenantStores.ListTenantStoreMenu ( )
            .Where<TenantVariableModel> ( m =>
            m.Variable == EnumTenantVariable.ProductSubCategory &&
            m.TenantStore == store ).ToList ( );


        return listSubCategory.ToList ( );
    }

    public static List<TenantVariableModel>
    GetSubCategoryListByID ( int categoryId,EnumStoreType store )
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();
        listSubCategory =
            TenantStores.ListTenantStoreMenu ( ).Where<TenantVariableModel>
            ( m =>
                m.Variable == EnumTenantVariable.ProductSubCategory &&
                m.ParentID == categoryId &&
                m.TenantStore == store ).ToList ( );

        return listSubCategory.ToList ( );
    }

    public static string GetTextForCategoryId
    ( int categoryId,EnumStoreType store )
    {
        List<TenantVariableModel> listCategory = GetCategoryList ( store );

        TenantVariableModel? tenantVariableModel =
                listCategory.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == categoryId);

        return tenantVariableModel!.Text;
    }

    public static string GetTextForSubCategoryId ( int subCategoryId,EnumStoreType store )
    {
        List<TenantVariableModel>  listSubCategory = new  List<TenantVariableModel> ();
        listSubCategory = GetSubCategoryList ( store );

        TenantVariableModel? tenantVariableModel =
                listSubCategory.FirstOrDefault<TenantVariableModel>
                ( m =>  m.ValueID == subCategoryId);

        return tenantVariableModel!.Text;
    }
}
