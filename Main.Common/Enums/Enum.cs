using System.ComponentModel;

namespace Main.Common;

public enum EnumCategoryFor
{
    DeshiHutBazar = 3,
    FineArts = 2,
    LifeStyles = 1
}

public enum EnumCompanyName
{
    [Description("Deshi Hut Bazar (Comapny Vendor)")]
    DeshiHutBazar = 1,

    [Description("Fine Arts Store (Company)")]
    FineArts = 2,

    [Description("Life Styles Store (Company)")]
    LifeStyles = 3,
}

public enum EnumCountry
{
    Bangladesh = 1
}

public enum EnumAllowedVariable
{
    Country = 0,
    State = 1,
    City = 2,
    Area = 3,
    Category = 4,
    SubCategory = 5
}

public enum EnumPhoto
{
    Carousel = 1,
    Thumbnail = 2,
    Square =  3,
    Rectangle = 4,
    Banner = 5,
    Other = 6
}

public enum EnumMarketType
{
    AllItems = 1,
    Category = 2,
    SubCategory = 3,
    Special = 4,
    SimilarItems = 5
}

public enum EnumCustomButtonItemType
{
    Men = 1,
    Women = 2,
    Kids = 3,
    Food = 4
}

public enum EnumReportLength
{
    [Description("Last 1 Hour")]
    LastOneHour = 1,
    [Description("Last 2 Hours")]
    LastTwoHour = 2,
    [Description("Last 3 Hours")]
    LastThreeHour = 3,
    [Description("Last 4 Hours")]
    LastFourHour = 4,
    [Description("Last 5 Hours")]
    LastFiveHour = 5,
    [Description("Last 6 Hours")]
    LastSixHour = 6,
    [Description("Last 12 Hours")]
    LastTwelveHour = 12,
    [Description("Last 18 Hours")]
    LastEighteenHour = 18,
    [Description("Today")]
    Today = 24,
    [Description("Last 1 Weeks")]
    LastOneWeek = 168,
    [Description("Last 2 Weeks")]
    LastTwoWeek = 336,
    [Description("Last 3 Weeks")]
    LastThreeWeek = 504,
    [Description("Last 1 Month")]
    LastOneMonth = 720,
    [Description("Last 1.5 Months")]
    LastOneAndHalfMonth = 1080,
    [Description("Last 2 Months")]
    LastTwoMonth = 1440,
    [Description("Last 3 Months")]
    LastThreeMonth = 2160,
    [Description("Last 1 Year")]
    LastOneYear = 8760
}

public enum EnumPaidBy
{
    DeshiHutBazar = 1,
    Company = 2,
    ServiceProvider = 3
}

public enum EnumWeekDays
{
    [Description("Sunday")]
    Sunday = 1,
    [Description("Monday")]
    Monday = 2,
    [Description("Tuesday")]
    Tuesday = 3,
    [Description("Wednesday")]
    Wednesday = 4,
    [Description("Thursday")]
    Thursday = 5,
    [Description("Friday")]
    Friday = 6,
    [Description("Saturday")]
    Saturday = 7
}

public enum EnumOfferType
{
    General = 1,
    Premium = 2
}

public enum EnumMarket
{
    [Description("Beauty")]
    Beauty = 1004,
    [Description("Health")]
    Health = 1005,
    [Description("Fashion for Women")]
    WomanFashion = 1006,
    [Description("Fashion for Men")]
    MenFashion = 1007,        
    [Description("Kids & Babies")]
    ToysKidsAndBabies = 1008,
    [Description("Fitness & Lifestyles")]
    FitnessAndLifeStyles = 1010
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

public enum EnumPostType
{
    [Description("Ad Space")]
    AdSpace = 2,

    [Description("Short Note")]
    ShortNote = 3,

    [Description("Youtube Video Link")]
    ShortVideo = 4,

    [Description("Product")]
    Product = 6
}


public enum EnumPanelStatus
{
    Saved = 1,
    Published = 2
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

public enum EnumRoles
{
    Admin = 1,
    User = 2,
    Company = 3,
    VisitorOnly = 4
}

public enum EnumDeviceType
{
    Desktop = 1,
    Mobile = 2,
    Tablet = 3
}

public enum EnumShowOrHide
{
    Yes = 1,
    No = 2
}

public enum EnumOrderStatus
{
    Saved = 0,
    Paid = 1,
    Approved = 2
}

public enum EnumCurrency
{
    [Description("Taka")]
    BDT = 2
}

public enum EnumLogType
{
    HomePageLink = 5,
    PostDetailLink = 1,
    AllItemMarketLink = 3,
    CategoryMarketLink = 4,
    SubCategoryMarketLink = 11,
    SearchMarketLink = 6,
    SimpleSearchLink = 7,
    AdvancedSearchLink = 8,
    SpecialMarketLink = 12,
    NoticePage = 13
}

public enum EnumState
{
    Dhaka=1,
    Chittagong=2,
    Khulna=3,
    Rajshahi=4,
    Barishal=5,
    Sylhet=6,
    Maimenshing=7,
    Rangpur=8
}

public enum EnumTransactionStatus
{
    AdminCheckPending = 0,
    AdminApproved = 1,        
    SystemApproved = 2
}

public enum EnumReasonForEmail
{
    VerifyEmailAddress = 1,
    ResetPassword = 2,
    AdLikedByUserEmail = 3,
    AdBrowsedByUser = 4,
    AdvertiserContactRequested = 5,
    UserMessaged = 6,
    Export = 7,
    Import = 8,
    Request = 9
}