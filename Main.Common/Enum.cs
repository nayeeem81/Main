using System.ComponentModel;

namespace Main.Common
{
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

    public enum EnumUserAccountType
    {
        IndividualAdvertiser = 1,
        Company = 2,
        Admin = 3,
        ContentAdmin = 4,
        FinancialAdmin = 5,
        CompanyCustomer = 6
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
        Customer = 2,
        ServiceProvider = 3
    }

    public enum EnumPartsPurchaseBy
    {
        DeshiHutBazar = 1,
        Customer = 2,
        ServiceProvider = 3
    }

    public enum EnumHomeServiceAvailable
    {
        Yes = 1,
        No = 2
    }

    public enum EnumWorkingHours
    {
        [Description("00:00 AM to 04:00 AM")]
        Slot1 = 1,
        [Description("04:00 AM to 08:00 AM")]
        Slot2 = 2,
        [Description("08:00 AM to 12:00 PM")]
        Slot3 = 3,
        [Description("12:00 PM to 04:00 PM")]
        Slot4 = 4,
        [Description("04:00 PM to 08:00 PM")]
        Slot5 = 5,
        [Description("08:00 PM to 12:00 PM")]
        Slot6 = 6
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

    public enum EnumStepNumber
    {
        Step1 = 1,
        Step2 =2,
        Step3 = 3,
        Step4 = 4,
        Step5 = 5,
        Step6 = 6,
        Step7 = 7,
        Step8 = 8,
        Step9 = 9,
        Step10 = 10,
        Step11 =11,
        Step12 =12,
        Step13 =13,
        Step14 =14
    }

    public enum EnumPostVisitAction
    {
        PostVisit = 0,
        PostLiked = 2,
        PostContactQueried = 3
    }

    public enum EnumOfferType
    {
        General = 1,
        Premium = 2
    }

    public enum EnumMarket
    {
        [Description("Electronics")]
        Electronics = 1001,
        [Description("TV & Home Appliances")]
        TVHomeAppliances = 1002,
        [Description("Home, Office & Living")]
        HomeAndLiving = 1003,
        [Description("Beauty")]
        Beauty = 1004,
        [Description("Health")]
        Health = 1005,
        [Description("Women's Fashion")]
        WomanFashion = 1006,
        [Description("Men's Fashion")]
        MenFashion = 1007,        
        [Description("Toys, Kids & Babies")]
        ToysKidsAndBabies = 1008,
        [Description("Motor")]
        Motor= 1009,
        [Description("Fitness & Lifestyles")]
        FitnessAndLifeStyles = 1010,
        [Description("Groceries & Pets")]
        GroceriesAndPets = 1011,
        [Description("Training, Hire & Travels")]
        TrainingHireTravels = 1012,
        [Description("Export & Import")]
        ExportImports = 1013,
        [Description("Others")]
        Others = 1014
    }

    public enum EnumSpecialMarket
    {
        /// <summary>
        /// ELECTRONICS
        /// </summary>
        [Description("Audio & Sound Systems")]
        AudioAndSoundSystems = 1001001,
        [Description("Camera and Accessories")]
        CameraAndAccesories = 1001002,
        [Description("Laptop and PC")]
        LaptopAndPCs = 1001003,        
        [Description("Mobile Phones")]
        MobilePhones = 1001004,
        [Description("Tablet & Gadgets")]
        TabletAndGadgets = 1001005,
        [Description("Phone Accessories")]
        PhoneAccessories = 1001006,
        
        /// <summary>
        /// TV & Home Appliances
        /// </summary>
        [Description("Kitchen Appliances")]
        KitchenAppliances = 1002001,
        [Description("TV & Home Audio")]
        TVAndHomeAudio = 1002002,
        [Description("Vacuum & Floor Care")]
        VacuumAndFloorCare = 1002003,       
        [Description("Cooling and Heating")]
        CoolingAndHeating = 1002004,
        [Description("Large Appliances")]
        LargeAppliances = 1002005,
        [Description("Clothing Care")]
        ClothingCare = 1002006,
        
        /// <summary>
        /// Home, Office & Living
        /// </summary>
        [Description("Diy & Cleaning")]
        DiyAndCleaning = 1003001,
        [Description("Kitchen & Dining")]
        KitchenAndDining = 1003002,
        [Description("Furniture")]
        Furniture = 1003003,
        [Description("Bath, Bed & Decor")]
        BathBedAndDecor = 1003004,
        [Description("Office Furniture")]
        OfficeFurniture = 1003005,

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
        [Description("Gold")]
        WomenGolds = 1006002,
        [Description("Women Dress")]
        WomenDresses = 1006003,
        [Description("Shoes")]
        WomenShoes = 1006004,
        [Description("Specticals")]
        WomenSpecticals = 1006005,
        [Description("Watches & Accessories")]
        WomenWatches = 1006006,
        [Description("Accessories")]
        WomenFashionAccessories = 1006007,
        [Description("Boishakhi Dress")]
        WomenBoishakDress = 1006008,
         
        /// <summary>
        /// Men Fashion
        /// </summary>
        [Description("Walets/Bags & Travel")]
        MenWaletBags = 1007001,
        [Description("Business Watches")]
        MenWatches = 1007002,
        [Description("Dress")]
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
        /// Motor
        /// </summary>
        [Description("Car Oils & Fluids")]
        CarOilFluids = 1009001,
        [Description("Car Essentials")]
        CarEssentials = 1009002,
        [Description("Motor Cycle Essentials")]
        MotorCycleEssentials = 1009003,
        [Description("Services and Installations")]
        CarServicesInstallations = 1009004,

        /// <summary>
        /// Fitnesses & Lifestyle
        /// </summary>
        [Description("Men's Sportswear")]
        MensSportswear = 1010001,
        [Description("Women's Sportswear")]
        WomensSportswear = 1010002,
        [Description("Fitness Equipments")]
        FitnessEquipments = 1010003,
        [Description("Global Fitness")]
        GlobalFittness = 1010004,
        [Description("Music, Books & Games")]
        MusicBooksGames = 1010005,

        /// <summary>
        /// Groceries & Pets
        /// </summary>
        [Description("Snacks & Cookies")]
        SnacksCookies = 1011001,
        [Description("Nuts")]
        Nuts = 1011002,
        [Description("Beverages")]
        Beverages = 1011003,
        [Description("Grocery Essentials")]
        GroceryEssentials = 1011004,
        [Description("Laundry")]
        Laundry = 1011005,
        [Description("Pets & Hobbies")]
        Pets = 1011006,

        /// <summary>
        /// Training, Hire & Travels
        /// </summary>
        [Description("Training Packages")]
        TrainingPackages = 1012001,
        [Description("Travel Packages")]
        TravelPackages = 1012002,
        [Description("Hire Me!")]
        HireMe = 1012003,

        /// <summary>
        /// Import & Export
        /// </summary>
        [Description("Export Products")]
        ExportProducts = 1013001,
        [Description("Import Products")]
        ImportProducts = 1013002,

        /// <summary>
        /// Others
        /// </summary>
        [Description("Other Product Items")]
        OtherProductItems = 1014001,
        [Description("Other Service Items")]
        OtherServiceItems = 1014002
    }

    public enum EnumPostType
    {
        [Description("Post")]
        Post = 1, 

        [Description("Ad Space")]
        AdSpace = 2,

        [Description("Short Note (Message, Text, Speech, Notice)")]
        ShortNote = 3,

        [Description("Short Video (Youtube)")]
        ShortVideo = 4,

        [Description("Service (FABIA)")]
        FabiaService = 5,

        [Description("Product")]
        Product = 6
    }


    public enum EnumGroupPanelStatus
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

        [Description("Ads Details Page")]
        AdsDetail = 3,

        [Description("Fabia Details Page")]
        FabiaDetail = 4,

        [Description("Post New Ad Page")]
        PostNewAd = 5,

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

        [Description("Login")]
        Login = 11,

        [Description("Register")]
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


    public enum EnumPanelDisplayStyle
    {
        [Description("Product (Star 4 Products)")]
        StarPanel = 10001,

        [Description("Triangle (3 Items)")]
        TrianglePanel = 10002,

        [Description("Sixer Group (6 Items)")]
        GroupSection = 10003,
        
        [Description("Videos & Comment")]
        VideoComment = 10004,

        [Description("Video & Post")]
        VideoPost = 10005,
        
        [Description("Market Panel")]
        MarketPanel = 10006,

        [Description("Similar Posts")]
        SimilarItemsMarketPanel = 10007,

        [Description("Quard (4 Items)")]
        QuardMarketPanel = 10008,

        [Description("Fabia Buttons")]
        AllFixedButtons = 10009,                       

        [Description("Popular Product")]
        MostPopularProduct = 10010,

        [Description("Popular Category")]
        PopularCategory = 10011,        

        [Description("Triple Block - 3 Layers (13 Items)")]
        DoubleBlockSquarePanel = 10012,

        [Description("Max Group (> 6 Items)")]
        MaxGroupSection = 10013,

        [Description("Banner Carousel (4 Items)")]
        BannerCarousel = 10014,

        [Description("Triangle Carousel (12 Items)")]
        TriangleCarousel = 10015,

        [Description("Star Carousel (16 Items)")]
        StarCarousel = 10016,

        [Description("Banner (1 Item)")]
        SingleBanner = 10017
    }

    public enum EnumImageCategory
    {
        [Description("Horizontal Rectengle")]
        HorizontalRectengle = 0,
        [Description("Vertical Rectengle")]
        VerticalRectengle = 1,
        [Description("Suqare")]
        Suqare = 2
    }

    public enum EnumColumn
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Six = 6
    }

    public enum EnumRoles
    {
        Admin = 1,
        Advertiser = 2,
        Public = 3,
        Company = 4
    }

    public enum EnumSelectionType
    {
        SystemPurchase = 1,
        AdminSelected = 2
    }

    public enum EnumDeviceType
    {
        Desktop = 1,
        Mobile = 2
    }

    public enum EnumShowOrHide
    {
        Yes = 1,
        No = 2
    }

    public enum EnumPackageOrderStatus
    {
        Saved = 0,
        Paid = 1,
        Approved = 2
    }

    public enum EnumCurrency
    {
        [Description("Malaysian Ringgit")]
        MYR = 0,
        [Description("US Dollar")]
        USD = 1,
        [Description("Bangladeshi Taka")]
        BDT = 2,
        [Description("Australian Dollar")]
        AUD = 3,
        [Description("Tanzanian Shilling")]
        TZS = 4,
        [Description("Canadian Dollar")]
        CAD = 5,
        [Description("Bangladeshn Naira")] 
        NGN = 6
    }

    public enum EnumAccountCredential
    {
        Valid = 0,
        Invalid = 1
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
        PortfolioLink = 9,
        FabiaServiceLink = 10,
        SpecialMarketLink = 12,
        NoticePage = 13
    }

   

    public enum EnumPostStatus
    {
        Payable = 0,
        FreePosted = 1,
        PaidPosted = 2,
        PremiumPaidPosted = 3,
        SubscriptionPosted = 4,
        PremiumSubscriptionPosted = 5
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

    public enum EnumPackageType
    {
        [Description("Startup Package")]
        StartUpPackage = 0,
        [Description("Advanced Package")]
        AdvancedPackage = 1,
        [Description("Public Package")]
        PuclicPackage = 2
    }

    public enum EnumPackageStatus
    {
        [Description("Active")]
        Enabled = 0,
        [Description("Disabled")]
        Disabled = 1
    }

    public enum EnumPackageSubscriptionPeriod
    {
        [Description("Month")]
        Month = 0,        
        [Description("Year")]
        Year = 1
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
}