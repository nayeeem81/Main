using System.ComponentModel;

namespace Main.Common.Enums;

public enum EnumShowOrHide
{
    Yes = 1,
    No = 2
}

public enum EnumPublicPage
{
    [Description("Home Page")]
    Home = 1,

    [Description("All Market Page")]
    AllMarket = 2,

    [Description("Details Page")]
    AdsDetail = 3,

    [Description("Category Button Page")]
    CategoryButtonMarket = 6,

    [Description("Category Dropdown Page")]
    SubCategoryDropdownMarket = 7,

    [Description("Special Button Page")]
    SpecialMarketButton = 8,

    [Description("News/Notice Page")]
    NoticeAndNews = 9,

    [Description("Resource Page")]
    Resources = 10,

    [Description("Login Page")]
    Login = 11,

    [Description("Register Page")]
    Signup = 12
}

public enum EnumPanelTemplate
{
    [Description("Product: (Double 2 Products)")]
    ProductDouble = 9,

    [Description("Product: (Quard 4 Products)")]
    ProductQuard = 1,

    [Description("Product: (Triangle 3 Products)")]
    ProductTriangle = 2,

    [Description("Product: (Sixer 6 Products)")]
    ProductSixer = 3,

    [Description("Product: (Popular Products)")]
    ProductMostPopular = 4,

    [Description("Product: (Market Page)")]
    MarketPanel = 5,

    [Description("Product: (Similar Products Panel)")]
    SimilarItemsMarketPanel = 6,

    [Description("Admin: (Banner Carousel: 4 Ad Spases)")]
    AdminBannerCarousel = 7,

    [Description("Admin: (Fixed Banner: 1 Ad Spases)")]
    AdminSingleBanner = 8
}


public enum EnumPostType
{
    [Description("Ad Space")]
    AdSpace = 1,

    [Description("Short Note")]
    ShortNote = 2,

    [Description("Youtube Video")]
    ShortVideo = 3,

    [Description("Product")]
    Product = 4
}

public enum EnumStoreType
{
    LifeStyles = 1,
    FineArts = 2,
    Sports = 3
}

public enum EnumTenantVariable
{
    [Description("Product Category")]
    ProductCategory = 1,

    [Description("Product Sub Category")]
    ProductSubCategory = 2
}

public enum EnumStoreMenu
{
    [Description("Beauty")]
    Beauty = 1,

    [Description("Health")]
    Health = 2,

    [Description("Fashion")]
    Fashion = 3,

    [Description("Lifestyles")]
    Fitness = 4,

    [Description("Arts")]
    ARTS = 5,

    [Description("Crafts")]
    CRAFTS = 6,

    [Description("Collectibles")]
    COLLECTIBLES = 7
}

public enum EnumStoreSubMenu
{
    [Description("Make up")]
    Makeup = 1,

    [Description("Skin care")]
    SkinCare = 2,

    [Description("Beauty tools")]
    BeautyTools = 3,

    [Description("Wellbeing")]
    Wellbeing = 4,

    [Description("Pharmacy product")]
    PharmacyProduct = 5,

    [Description("Medical supplies")]
    MedicalSupplies = 6,

    [Description("Sculpture")]
    Sculpture = 7,

    [Description("Drawing")]
    Drawing = 8,

    [Description("Water color")]
    WaterColor = 9,

    [Description("Photography")]
    Photography = 10,

    [Description("Painting")]
    Painting = 11
}

public enum EnumPostCount
{
    [Description("1")]
    PostCountOne = 1,

    [Description("2")]
    PostCountTwo = 2,

    [Description("3")]
    PostCountThree = 3,

    [Description("4")]
    PostCountFour = 4,

    [Description("6")]
    PostCountSix = 6
}

public enum EnumIsValidTemplate
{
    [Description("Template can display in panel")]
    ExactMatchValid = 1,

    [Description("Template has more posts than required. Template needs moderating!")]
    GreaterMatchValid = 2,

    [Description("Template has wrong configuration!")]
    Invalid = 3
}

public enum EnumCountry
{
    Bangladesh = 1
}

public enum EnumSpecialMarket
{
    /// <summary>
    /// Beauty
    /// </summary>
    [Description("Makeup")]
    Makeup = 1004001,
    [Description("Skin Care")]
    SkinCare = 1004002,
    [Description("Hair, Bath & Body")]
    HairBathBody = 1004003,
    [Description("Beauty Tools")]
    BeautyTools = 1004004,

    /// <summary>
    /// Health
    /// </summary>
    [Description("Wellbeing")]
    Wellbeings = 1005001,
    [Description("Beauty Suppliments")]
    BeautySupplements = 1005002,
    [Description("Medical Supplies")]
    MedicalSupplies = 1005003,
    [Description("Personal Care")]
    PersonalCare = 1005004,
    [Description("Pharmacy Products")]
    PharmacyProducts = 1005005,

    /// <summary>
    /// Woman Fashion
    /// </summary>
    [Description("Bags")]
    WomenBags = 1006001,
    [Description("Gold & Others")]
    WomenGolds = 1006002,
    [Description("Cloth & Dress")]
    WomenDresses = 1006003,
    [Description("Boishakhi Dress")]
    WomenBoishakDress = 1006008,
    [Description("Shoes")]
    WomenShoes = 1006004,
    [Description("Specticals")]
    WomenSpecticals = 1006005,
    [Description("Watch")]
    WomenWatches = 1006006,
    [Description("Accessories")]
    WomenFashionAccessories = 1006007,


    /// <summary>
    /// Men Fashion
    /// </summary>
    [Description("Walet & Bag")]
    MenWaletBags = 1007001,
    [Description("Watches")]
    MenWatches = 1007002,
    [Description("Cloth & Dress")]
    MenDresses = 1007003,
    [Description("Shoes")]
    MenShoes = 1007004,
    [Description("Specticals")]
    Specticals = 1007005,
    [Description("Accessories")]
    MenFashionAccessories = 1007006,

    /// <summary>
    /// Toy, Kids & Babies
    /// </summary>
    /// 
    [Description("Baby Clothing & Maternity")]
    BabyMaternity = 1008001,
    [Description("Baby Gear & Nursery")]
    BabyGears = 1008002,
    [Description("Diapers")]
    BabyDiapers = 1008003,
    [Description("Toys & Games")]
    ToysGames = 1008004,
    [Description("Feeding")]
    Feeding = 1008005,
    [Description("Milk Formula")]
    MilkFormula = 1008006,

    /// <summary>
    /// Fitnesses & Lifestyle
    /// </summary>
    [Description("Sportswear")]
    MensSportswear = 1010001,
    [Description("Fitness Equipments")]
    FitnessEquipments = 1010003,
    [Description("Sports & Fitness Tools")]
    GlobalFittness = 1010004,
    [Description("Music, Book & Games")]
    MusicBooksGames = 1010005
}

public enum EnumCurrency
{
    [Description("Taka")]
    BDT = 2
}