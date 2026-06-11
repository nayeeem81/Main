using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Data.Migrations.Business
{
    /// <inheritdoc />
    public partial class InitialBusinessMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdPosts",
                columns: table => new
                {
                    AdminPostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    PosterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterContactNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortNote = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    SearchTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdPosts", x => x.AdminPostID);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnumPublicPage = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    SubCategoryID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SaleCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SearchTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "AdImageFiles",
                columns: table => new
                {
                    AdminImageFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AdminPostID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdImageFiles", x => x.AdminImageFileID);
                    table.ForeignKey(
                        name: "FK_AdImageFiles_AdPosts_AdminPostID",
                        column: x => x.AdminPostID,
                        principalTable: "AdPosts",
                        principalColumn: "AdminPostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdPostComments",
                columns: table => new
                {
                    AdminPostCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPostID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdPostComments", x => x.AdminPostCommentID);
                    table.ForeignKey(
                        name: "FK_AdPostComments_AdPosts_AdminPostID",
                        column: x => x.AdminPostID,
                        principalTable: "AdPosts",
                        principalColumn: "AdminPostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Panels",
                columns: table => new
                {
                    PanelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelPosition = table.Column<int>(type: "int", nullable: false),
                    PanelTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelTemplate = table.Column<int>(type: "int", nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panels", x => x.PanelID);
                    table.ForeignKey(
                        name: "FK_Panels_Pages_PageID",
                        column: x => x.PageID,
                        principalTable: "Pages",
                        principalColumn: "PageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    ProductCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.ProductCommentID);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImageFiles",
                columns: table => new
                {
                    ProductImageFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageFiles", x => x.ProductImageFileID);
                    table.ForeignKey(
                        name: "FK_ProductImageFiles_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    EnumPostType = table.Column<int>(type: "int", nullable: false),
                    RootID = table.Column<int>(type: "int", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanelID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Panels_PanelID",
                        column: x => x.PanelID,
                        principalTable: "Panels",
                        principalColumn: "PanelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageID", "CreatedBy", "CreatedDate", "EnumPublicPage", "HostCompanyName", "HostCountry", "IdentityUserId", "IsActive", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdImageFiles_AdminPostID",
                table: "AdImageFiles",
                column: "AdminPostID");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostComments_AdminPostID",
                table: "AdPostComments",
                column: "AdminPostID");

            migrationBuilder.CreateIndex(
                name: "IX_Panels_PageID",
                table: "Panels",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PanelID",
                table: "Posts",
                column: "PanelID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductID",
                table: "ProductComments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_ProductID",
                table: "ProductImageFiles",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdImageFiles");

            migrationBuilder.DropTable(
                name: "AdPostComments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImageFiles");

            migrationBuilder.DropTable(
                name: "AdPosts");

            migrationBuilder.DropTable(
                name: "Panels");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
