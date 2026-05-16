using Main.Common;
using Main.Common.Model;

namespace Main.Common.HelperServices;

public static class BusinessSeedFineArts
{
    public static List<ParentChildVriableModel> FineArtsBusinessSeed ( )
    {
        List<ParentChildVriableModel> ListEnglishAValues = new List<ParentChildVriableModel>()
        {
            new ParentChildVriableModel
            {
                ValueID = 1, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = "ARTS", IconLink= "fas fa-palette"
            },

            new ParentChildVriableModel
            {
                ValueID = 2, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = "CRAFTS", IconLink= "fas fa-cubes"
            },

            new ParentChildVriableModel
            {
                ValueID = 3, ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = "COLLECTIBLES", IconLink= "fas fa-" }
            };

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 5,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Painting",
            IconLink = "fa fa-car-alt link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 6,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Sculpture",
            IconLink = "fa fa-car-alt link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 7,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Drawing",
            IconLink = "fa fa-car-alt link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 8,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Water Color",
            IconLink = "fa fa-car-alt link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 9,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Photography",
            IconLink = "fa fa-car-alt link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 9,
            ParentValueID = 1,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Photography",
            IconLink = "fa fa-car-alt link-dark"
        } );

        //Crafts
        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 10,
            ParentValueID = 2,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Jute",
            IconLink = "fa fa-cubes link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 11,
            ParentValueID = 2,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Handicrafts",
            IconLink = "fa fa-cubes link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 12,
            ParentValueID = 2,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Metal",
            IconLink = "fa fa-cubes link-dark"
        } );

        //Collectibles
        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 13,
            ParentValueID = 3,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Stationery",
            IconLink = "fa fa-pen link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 14,
            ParentValueID = 3,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Coins & Currency",
            IconLink = "fa fa-pen link-dark"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = 15,
            ParentValueID = 3,
            Variable = EnumAllowedVariable.SubCategory,
            Text = "Stamps",
            IconLink = "fa fa-pen link-dark"
        } );

        return ListEnglishAValues;
    }

    public static List<ParentChildVriableModel> GetCategoryList(EnumCategoryFor categoryFor)
    {
        if(categoryFor == EnumCategoryFor.FineArts)
        {
            return FineArtsBusinessSeed ( )
                    .ToList()
                    .Where(a => a.Variable == EnumAllowedVariable.Category)
                    .ToList<ParentChildVriableModel>();
        }

        return new List<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> GetSubCategoryList(EnumCategoryFor categoryFor)
    {
        if(categoryFor == EnumCategoryFor.FineArts)
        {
            return FineArtsBusinessSeed ( )
                    .ToList()
                    .Where(a => a.Variable ==       EnumAllowedVariable.SubCategory)
                    .ToList<ParentChildVriableModel>();
        }

        return new List<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> GetSubCategoryListByCategoryID (
                    int parentId,               
                    EnumCategoryFor categoryFor)
    {
        if (categoryFor == EnumCategoryFor.FineArts)
        {
            return FineArtsBusinessSeed ( )                    
                .ToList ()
                .Where(a =>
                    a.Variable == EnumAllowedVariable.SubCategory &&
                    a.ParentValueID == parentId)
                .ToList<ParentChildVriableModel>();
        }

        return new List<ParentChildVriableModel>();
    }

    public static string GetItemText(
            int? id, 
            EnumCategoryFor categoryFor)
    {
        if ( !id.HasValue )
            return "";

        if(categoryFor == EnumCategoryFor.FineArts)
        {
            var obj = FineArtsBusinessSeed()
                    .FirstOrDefault <ParentChildVriableModel> 
                     (a => a.ValueID == id);
            
            return obj != null ? obj.Text : "";
        }

        return "";
    }

    public static string GetCSS(
            int? id, 
            EnumCategoryFor categoryFor)
    {
        if (!id.HasValue)
            return "";

        if (id.HasValue && id.Value == 0)
            return "";

        if(categoryFor == EnumCategoryFor.FineArts)
        {
           var obj = FineArtsBusinessSeed()
                    .ToList()
                    .FirstOrDefault(a => a.ValueID == id);

            return obj != null ? obj.IconLink : "";
        }   
       
        return "";
    }

    public static List<ParentChildVriableModel> 
        GetSubCategoryList()
    {
        return FineArtsBusinessSeed ( ).ToList()
                .Where ( a => 
                    a.Variable == EnumAllowedVariable.SubCategory)
                .ToList<ParentChildVriableModel>(); 
    }

    public static List<ParentChildVriableModel> GetCategoryList()
    {
        return FineArtsBusinessSeed ( )
                .ToList()
                .Where(a => 
                       a.Variable == EnumAllowedVariable.Category)
                .ToList<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> GetByVariableAndParent(EnumAllowedVariable variable, long parentId, EnumCategoryFor categoryFor)
    {
        if (categoryFor == EnumCategoryFor.FineArts)
        {
            return FineArtsBusinessSeed ( )
                     .Where(a => a.Variable == variable &&
                            a.ParentValueID == parentId)
                            .OrderBy(order => order.ValueID)
                            .ToList();
        }

        return new List<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> GetListByVariable(EnumAllowedVariable variable, EnumCategoryFor categoryFor)
    {
        if (categoryFor == EnumCategoryFor.FineArts)
        {
            return FineArtsBusinessSeed().Where(a => 
                a.Variable == variable).OrderBy(order => order.ValueID).ToList();
        }

        return new List<ParentChildVriableModel>();
    }

    public static string GetItemTextById(int valueId, EnumCategoryFor categoryFor)
    {
        if (valueId == 0)
            return "";

        if (categoryFor == EnumCategoryFor.FineArts) 
        {
            var obj = FineArtsBusinessSeed()
                     .SingleOrDefault(a => a.ValueID == valueId);
            
            return obj != null ? obj.Text : "";
        }

        return "";
    }

    public static long GetCatIDBySubCategoryID(
                        int valueId, 
                        EnumCategoryFor categoryFor)
    {
        if (valueId == 0)
            return 0;

        if (categoryFor == EnumCategoryFor.FineArts)
        {
            var obj = FineArtsBusinessSeed()
                .SingleOrDefault
                 (a => a.ValueID == valueId);
            
            return obj != null ? obj.ParentValueID : 0;
        }

        return 0;
    }
}
