using Main.Common;
using Main.Common.Model;

namespace Main.Common.Helper;

public static class BusinessSeedLifeStyle
{
    public static List<ParentChildVriableModel> LifeStyleBusinessSeed ( )
    {
        List<ParentChildVriableModel> ListEnglishAValues = new List<ParentChildVriableModel>()
        {
            //category
            new ParentChildVriableModel()
            {
                ValueID = (int) EnumMarket.Beauty , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["Beauty"], IconLink= "fas fa-user-injured"
            },

            new ParentChildVriableModel()
            {
                ValueID = (int) EnumMarket.Health,
                ParentValueID = 0,
                Variable = EnumAllowedVariable.Category,
                Text = GlobalResources.Localizer["Health"], IconLink= "fas fa-briefcase-medical"
            },

            new ParentChildVriableModel()
            {
                ValueID = (int) EnumMarket.WomanFashion , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["WomanFashion"] , IconLink= "fas fa-venus-double"
            },

            new ParentChildVriableModel()
            {
                ValueID = (int) EnumMarket.MenFashion , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["MenFashion"] , IconLink= "fas fa-user"
            },
            new ParentChildVriableModel()
            {
                ValueID = (int) EnumMarket.ToysKidsAndBabies , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["ToysKidsAndBabies"] , IconLink= "fas fa-child"
            },

            new ParentChildVriableModel()
            {
                ValueID = (int) EnumMarket.FitnessAndLifeStyles , ParentValueID = 0, Variable = EnumAllowedVariable.Category, Text = GlobalResources.Localizer["FitnessAndLifeStyles"] , IconLink= "fa fa-baseball-ball"
            }
        };


        // Category
        var beauty = (int) EnumMarket.Beauty;
        var health = (int) EnumMarket.Health;
        var womenfashion = (int) EnumMarket.WomanFashion;
        var menfashion = (int) EnumMarket.MenFashion;
        var toyskidsandbabies = (int)EnumMarket.ToysKidsAndBabies;
        var fitnessandlifestyle = (int)EnumMarket.FitnessAndLifeStyles;


        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.Makeup,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Makeup"],
            IconLink = "fa fa fa-home"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["SkinCare"],
            IconLink = "fa fa fa-home"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.HairBathBody,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["HairBathBody"],
            IconLink = "fa fa fa-home"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.BeautyTools,
            ParentValueID = beauty,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BeautyTools"],
            IconLink = "fa fa fa-home"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.Wellbeings,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Wellbeings"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.PharmacyProducts,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Pharmacy"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MedicalSupplies,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MedicalSupplies"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.BeautySupplements,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BeautySupplements"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.PersonalCare,
            ParentValueID = health,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["PersonalCare"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenBags,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenBags"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenGolds,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenGolds"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenDresses,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenDresses"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenShoes,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenShoes"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenSpecticals,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Specticals"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenWatches,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenWatches"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenFashionAccessories,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenFashionAccessories"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenBoishakDress,
            ParentValueID = womenfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomenFashionWomenBoishakDress"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MenWaletBags,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenWaletBags"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MenWatches,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenWatches"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MenDresses,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenDresses"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MenShoes,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenShoes"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.Specticals,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["Specticals"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MenFashionAccessories,
            ParentValueID = menfashion,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MenFashionAccessories"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.BabyMaternity,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BabyMaternity"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.BabyGears,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BabyGears"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.BabyDiapers,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["BabyDiapers"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.ToysGames,
            ParentValueID = toyskidsandbabies,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["ToysGames"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MensSportswear,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MensSportswear"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.WomenDresses,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["WomensSportswear"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.FitnessEquipments,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["FitnessEquipments"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.GlobalFittness,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["GlobalFittness"],
            IconLink = "fa fa fa-laptop"
        } );

        ListEnglishAValues.Add ( new ParentChildVriableModel ( )
        {
            ValueID = ( int ) EnumSpecialMarket.MusicBooksGames,
            ParentValueID = fitnessandlifestyle,
            Variable = EnumAllowedVariable.SubCategory,
            Text = GlobalResources.Localizer["MusicBooksGames"],
            IconLink = "fa fa fa-laptop"
        } );

        return ListEnglishAValues;
    }
    
    public static List<ParentChildVriableModel> GetCategoryList(EnumCategoryFor categoryFor)
    {
        if(categoryFor == EnumCategoryFor.LifeStyles)
        {
            return LifeStyleBusinessSeed ( )
                .ToList()
                .Where( a => a.Variable ==      EnumAllowedVariable.Category)
                .ToList<ParentChildVriableModel>();
        }

        return new List<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> 
        GetSubCategoryList(EnumCategoryFor categoryFor)
    {
        if(categoryFor == EnumCategoryFor.LifeStyles)
        {
            return LifeStyleBusinessSeed ( )
                   .Where ( a => a.Variable 
                              == EnumAllowedVariable.SubCategory )
                   .ToList<ParentChildVriableModel>();
        }

        return new List<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> GetSubCategoryListByCategoryID (
                int parentId, 
                EnumCategoryFor categoryFor)
    {
        if(categoryFor == EnumCategoryFor.LifeStyles)
        {
            return LifeStyleBusinessSeed ( )
                .Where(a => 
                       a.Variable ==                       EnumAllowedVariable.SubCategory &&
                       a.ParentValueID == parentId)
                .ToList<ParentChildVriableModel>();
        } 

        return new List<ParentChildVriableModel>();
    }

    public static string GetItemText (
            int? id, 
            EnumCategoryFor categoryFor)
    {
        if (!id.HasValue)
            return "";

        if(categoryFor == EnumCategoryFor.LifeStyles)
        {
            var obj = LifeStyleBusinessSeed()
                     .FirstOrDefault(a => a.ValueID == id);
            
            if ( obj == null)
                return "";
            
            return obj.Text;
        }

        return "";
    }

    public static string GetCSS(int? id, EnumCategoryFor categoryFor)
    {
        if (!id.HasValue)
            return "";

        if (id.HasValue && id.Value == 0)
            return "";

        if ( categoryFor == EnumCategoryFor.LifeStyles )
        {
            var obj = LifeStyleBusinessSeed()
                     .FirstOrDefault(a => a.ValueID == id);

            if ( obj == null )
                return "";

            return obj.IconLink;
        }

        return "";
    }

    public static List<ParentChildVriableModel> GetCategoryList ( )
    {
        return LifeStyleBusinessSeed ( )
              .Where ( a => a.Variable == EnumAllowedVariable.SubCategory )
              .ToList<ParentChildVriableModel> ( );
    }

    public static List<ParentChildVriableModel> GetSubCategoryList ( )
    {
        return LifeStyleBusinessSeed ( )
              .Where(
                        a => a.Variable == EnumAllowedVariable.Category)
              .ToList<ParentChildVriableModel>();
    }


    public static List<ParentChildVriableModel> GetByVariableAndParent ( EnumAllowedVariable variable, int parentId, EnumCategoryFor categoryFor)
    {
        if (categoryFor == EnumCategoryFor.LifeStyles)
        {
            return LifeStyleBusinessSeed()
                .Where(a => 
                       a.Variable == variable && a.ParentValueID == parentId)
                .OrderBy(order => order.ValueID)
                .ToList();
        }

        return new List<ParentChildVriableModel>();
    }

    public static List<ParentChildVriableModel> 
        GetListByVariable ( EnumAllowedVariable variable, 
        EnumCategoryFor categoryFor)
    {
        if(categoryFor == EnumCategoryFor.LifeStyles)
        {
            return LifeStyleBusinessSeed()
                .Where(a => a.Variable == variable)
                .OrderBy(order => order.ValueID)
                .ToList();
        }
        
        return new List<ParentChildVriableModel>();
    }

    public static string GetItemTextById ( int valueId, EnumCategoryFor categoryFor)
    {
        if (valueId == 0)
            return "";

        if (categoryFor == EnumCategoryFor.LifeStyles) 
        {
            var obj = LifeStyleBusinessSeed()
                     .SingleOrDefault
                      (a => a.ValueID == valueId);
            
            return obj != null ? obj.Text : "";
        }

        return "";
    }

    public static int GetCategoryIDForSubCategoryID(int valueId, EnumCategoryFor categoryFor)
    {
        if (valueId == 0)
            return 0;

        if (categoryFor == EnumCategoryFor.LifeStyles)
        {
            var obj = LifeStyleBusinessSeed()
                     .SingleOrDefault(a => a.ValueID == valueId);
            
            return obj != null ? obj.ParentValueID : 0;

        }

        return 0;
    }
}
