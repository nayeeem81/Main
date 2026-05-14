using Microsoft.EntityFrameworkCore;
using Main.Common;

namespace Main.Model
{
    public class WebBusinessEntityContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Model.Context.WebBusinessEntityContext>());


        // Constructor used by DI/EF Core
        public WebBusinessEntityContext(DbContextOptions<WebBusinessEntityContext> options) : base(options) { }

        public DbSet<Page> Pages { get; set; }

        public DbSet<PageContent> PageContents { get; set; }

        public DbSet<PagePanel> PagePanels { get; set; }

        public DbSet<PanelPost> PanelPosts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImageFile> ProductImageFiles { get; set; }

        public DbSet<ProductComment> ProductComments { get; set; }

        public DbSet<AdminPost> AdminPosts { get; set; }

        public DbSet<AdminPostComment> AdminPostComments { get; set; }

        public DbSet<AdminImageFile> AdminImageFiles { get; set; }

        public DbSet<AValue> AValues { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            FineArtsCompanyUserSeed(modelBuilder);

            PageSeed(modelBuilder);
        }

        private void FineArtsCompanyUserSeed(ModelBuilder modelBuilder)
        {
            var adminUserFineArtsCreator = new User("e03fd0d4-00fd-090a-ca10-0d00a1118ba4", "naimul.prodhan@gmail.com", "Developer");
            adminUserFineArtsCreator.UserID = 1;
            adminUserFineArtsCreator.CreatedBy = int.MaxValue;
            adminUserFineArtsCreator.ModifiedBy = int.MaxValue;
            modelBuilder.Entity<User>().HasData(adminUserFineArtsCreator);

            var adminUserFineArts = new User("e03fd0e4-00fd-090a-ca10-0d00a0018ba4", "syedron@gmail.com", "FineArts");
            adminUserFineArts.UserID = 2;
            modelBuilder.Entity<User>().HasData(adminUserFineArts);


            var companyUserFineArts = new User("e03fd0e4-00fd-090a-ca10-0d00a0018ba5", "finearts@gmail.com", "Fine Arts");
            companyUserFineArts.UserID = 3;
            modelBuilder.Entity<User>().HasData(companyUserFineArts);
        }

        private void PageSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.Home, 1));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.AdsDetail, 2));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.Signup, 3));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.Login, 4));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.AllMarket, 5));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.SubCategoryDropdownMarket, 6));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.FabiaDetail, 7));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.CategoryButtonMarket, 8));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.NoticeAndNews, 9));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.PostNewAd, 10));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.Resources, 11));

            modelBuilder.Entity<Page>().HasData(new Page(EnumPublicPage.SpecialMarketButton, 12));
        }
    }
}