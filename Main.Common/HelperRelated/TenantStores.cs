using Main.Common.Enums;
using Main.Common.Model;

using ResourceLibrary.Resources;
namespace Main.Common.Helper;

public class TenantStores
{
    public TenantStores ( )
    {
    }

    public static List<TenantVariableModel> ListTenantStoreMenu ( )
    {
        List<TenantVariableModel> ListTenantStoreMenu
        = new List<TenantVariableModel>();


        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.Beauty,
            ParentID = ( int ) EnumStoreMenu.Beauty,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Beauty"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.Health,
            ParentID = ( int ) EnumStoreMenu.Health,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Health"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.Fashion,
            ParentID = ( int ) EnumStoreMenu.Fashion,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["Fashion"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.Fitness,
            ParentID = ( int ) EnumStoreMenu.Fitness,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["FitnessAndLifeStyles"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.ARTS,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["ARTS"],
            TenantStore = EnumStoreType.FineArts,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.CRAFTS,
            ParentID = ( int ) EnumStoreMenu.CRAFTS,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["CRAFTS"],
            TenantStore = EnumStoreType.FineArts,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreMenu.COLLECTIBLES,
            ParentID = ( int ) EnumStoreMenu.COLLECTIBLES,
            Variable = EnumTenantVariable.ProductCategory,
            Text = GlobalResources.Localizer["COLLECTIBLES"],
            TenantStore = EnumStoreType.FineArts,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.Makeup,
            ParentID = ( int ) EnumStoreMenu.Beauty,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Makeup"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.SkinCare,
            ParentID = ( int ) EnumStoreMenu.Beauty,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["SkinCare"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.BeautyTools,
            ParentID = ( int ) EnumStoreMenu.Beauty,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["BeautyTools"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.Wellbeing,
            ParentID = ( int ) EnumStoreMenu.Health,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Wellbeings"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.PharmacyProduct,
            ParentID = ( int ) EnumStoreMenu.Health,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Pharmacy"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.MedicalSupplies,
            ParentID = ( int ) EnumStoreMenu.Health,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["MedicalSupplies"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.Painting,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Painting"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.Drawing,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Drawing"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.Sculpture,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Sculpture"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.Photography,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["Photography"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        ListTenantStoreMenu.Add ( new TenantVariableModel ( )
        {
            ValueID = ( int ) EnumStoreSubMenu.WaterColor,
            ParentID = ( int ) EnumStoreMenu.ARTS,
            Variable = EnumTenantVariable.ProductSubCategory,
            Text = GlobalResources.Localizer["WaterColor"],
            TenantStore = EnumStoreType.LifeStyles,
            TenantId = ""
        } );

        return ListTenantStoreMenu;
    }
}
