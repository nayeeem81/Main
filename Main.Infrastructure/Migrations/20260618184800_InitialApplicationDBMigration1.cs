using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialApplicationDBMigration1 : Migration
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdPosts", x => x.AdminPostID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AValue",
                columns: table => new
                {
                    ValueID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variable = table.Column<int>(type: "int", nullable: false),
                    ParentValueId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostCompanyName = table.Column<int>(type: "int", nullable: false),
                    HostCountry = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AValue", x => x.ValueID);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                table: "ApplicationRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "TenantId" },
                values: new object[,]
                {
                    { "d02fd0e4-10fd-090a-ca30-0d00a0038ba0", null, "Admin", "ADMIN", "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { "d02fd0e4-20fd-090a-ca30-0d00a0038ba4", null, "User", "USER", "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { "d02fd0e4-30fd-090a-ca30-0F00a0898ba4", null, "SuperAdmin", "SUPERADMIN", "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { "e02fd0e4-00fd-090a-ca30-0d00a0038ba0", null, "Admin", "ADMIN", "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { "e02fd0e4-00fd-090a-ca30-0d00a0038ba4", null, "User", "USER", "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { "e02fd0e4-00fd-090a-ca30-0F00a0898ba4", null, "SuperAdmin", "SUPERADMIN", "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e03fd0d4-00fd-090a-ca10-0d00a1118ba4", 0, "abae671a-329f-4577-894d-b22610360eb3", "naimul.prodhan@gmail.com", true, false, null, "NAIMUL.PRODHAN@GMAIL.COM", "SUPERADMIN1", "AQAAAAIAAYagAAAAEIQl9FBFD3yJyJUrZKLTVLZXh7g9EBbHRrW3waK5uprkAW3VGD8qy62IPytPu5eXhQ==", null, false, "a80b4766-3125-47a5-acd0-1669977f9987", "e02fd0e1-00fd-009a-ca30-0d00a2345ba0", false, "SuperAdmin1" },
                    { "e03fd0d4-00fd-090a-da10-0d00a2228ba4", 0, "7f17a283-2a14-42a1-a560-818b679f0121", "naimul.prodhan@gmail.com", true, false, null, "NAIMUL.PRODHAN@GMAIL.COM", "SUPERADMIN2", "AQAAAAIAAYagAAAAEI+/FiLhk07GD3bVI4BtjrIIbciLCOPgoQaXxZF+OKt5FfNE6mcXlVUsq8CrNE+Fyw==", null, false, "94dd4490-be9a-4410-93e4-de6593948453", "e02fd0e1-00fd-008a-ca30-5d00a5242ba0", false, "SuperAdmin2" },
                    { "e03fd0e4-00fd-090a-ca10-0d00a0018ba4", 0, "170f8f54-c18a-4e75-8044-9349d7130d1f", "syedron@gmail.com", true, false, null, "SYEDRON@GMAIL.COM", "ADMIN1", "AQAAAAIAAYagAAAAEKJS65A1cB34h7uFLIIu2D6MiKIWQnjaDpicjAof0WK9nPcEkW8FfvTtqAQ2TrfWGA==", null, false, "e5a6e5e3-f95a-428d-818e-baf141de982b", "e02fd0e1-00fd-008a-ca30-5d00a5242ba0", false, "Admin1" },
                    { "e03fd0e4-55fd-095a-ca10-0d00a0018ba4", 0, "23a83d5d-aba1-46fb-ab99-c5c9259814a1", "syedron@gmail.com", true, false, null, "SYEDRON@GMAIL.COM", "ADMIN2", "AQAAAAIAAYagAAAAEEn7Uonuc9wxnmDG92bXB5/8CDcx3uqun5yqKV6cBn7S7x80f64ElPV/7bZzIJ31sg==", null, false, "1db03677-9a86-4e40-a728-4cff0b0952ab", "e02fd0e1-00fd-008a-ca30-5d00a5242ba0", false, "Admin2" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<string>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "e02fd0e4-00fd-090a-ca30-0F00a0898ba4", "e03fd0d4-00fd-090a-ca10-0d00a1118ba4" },
                    { "d02fd0e4-30fd-090a-ca30-0F00a0898ba4", "e03fd0d4-00fd-090a-da10-0d00a2228ba4" },
                    { "e02fd0e4-00fd-090a-ca30-0d00a0038ba0", "e03fd0e4-00fd-090a-ca10-0d00a0018ba4" },
                    { "d02fd0e4-10fd-090a-ca30-0d00a0038ba0", "e03fd0e4-55fd-095a-ca10-0d00a0018ba4" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageID", "CreatedBy", "CreatedDate", "EnumPublicPage", "HostCompanyName", "HostCountry", "IdentityUserId", "IsActive", "ModifiedBy", "ModifiedDate", "TenantId" },
                values: new object[,]
                {
                    { 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 2, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 3, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 4, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 5, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 6, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 7, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 8, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 9, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 10, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-009a-ca30-0d00a2345ba0" },
                    { 11, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 12, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 13, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 14, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 15, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 16, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 17, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 18, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 19, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" },
                    { 20, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e02fd0e1-00fd-008a-ca30-5d00a5242ba0" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "Domain", "Name", "ShopType" },
                values: new object[,]
                {
                    { "e02fd0e1-00fd-008a-ca30-5d00a5242ba0", "lifestyle-local", "LifeStyle Store", 1 },
                    { "e02fd0e1-00fd-009a-ca30-0d00a2345ba0", "fanarts-local", "Fine Arts Store", 2 }
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
                name: "RoleNameIndex",
                table: "ApplicationRoles",
                columns: new[] { "NormalizedName", "TenantId" },
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ApplicationUsers",
                columns: new[] { "NormalizedEmail", "TenantId" },
                unique: true,
                filter: "[NormalizedEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ApplicationUsers",
                columns: new[] { "NormalizedUserName", "TenantId" },
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "ApplicationRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "AValue");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<string>");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImageFiles");

            migrationBuilder.DropTable(
                name: "Tenants");

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
