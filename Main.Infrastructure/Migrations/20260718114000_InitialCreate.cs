using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdPosts", x => x.AdminPostID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AValues",
                columns: table => new
                {
                    ValueID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Variable = table.Column<int>(type: "int", nullable: false),
                    ParentValueId = table.Column<long>(type: "bigint", nullable: false),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AValues", x => x.ValueID);
                });

            migrationBuilder.CreateTable(
                name: "EmailOutboxMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExceptionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    ErrorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DetailedMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerException = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RequestHeaders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Environment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResolutionNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnumPublicPage = table.Column<int>(type: "int", nullable: false),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SaleCommission = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    SearchTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "TenantInvitations",
                columns: table => new
                {
                    InviteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvitedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantInvitations", x => x.InviteId);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantHostType = table.Column<int>(type: "int", nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Store = table.Column<int>(type: "int", nullable: false),
                    SmtpId = table.Column<int>(type: "int", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantUsers",
                columns: table => new
                {
                    TenantUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUsers", x => x.TenantUserId);
                    table.ForeignKey(
                        name: "FK_TenantUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "EmailSmtps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    FkTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSmtps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailSmtps_Tenants_FkTenantId",
                        column: x => x.FkTenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId");
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
                    SessionUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantCountry = table.Column<int>(type: "int", nullable: true),
                    TenantContinent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "GlobalAdmin", null, "GlobalAdmin", "GLOBALADMIN" },
                    { "User", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000002-0000-0000-0000-000000000000", 0, "0860f796-6a62-4b00-9383-a31ae956f6ae", "admin@system.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEMla/2PdDqa4uFuZBKBQJVp2GhRUv1PmgcNnSCPwVg7Vi4p0jCuKQlngVBmT2wmXHQ==", null, false, "38605e23-aaa1-4c6b-a83c-d5c33cb69d02", false, "admin@system.com" },
                    { "00000003-0000-0000-0000-000000000000", 0, "7af91095-afec-4622-9eba-5fb00fcdcbbb", "tenant1.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPpsv0fLvcvh7I2vLgPxs/pRzp+WzJ7tYxNsKsN01OC1FTYIo6wUrjRwVmpBHyRNtw==", null, false, "e2cead54-cde5-49bf-922f-88a87aa85950", false, "tenant1.admin@test.com" },
                    { "00000004-0000-0000-0000-000000000000", 0, "ae65f95b-bcc0-444f-9e9c-ec33e2d4b3e6", "tenant1.content@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEMkTFa8+xo/NXMV7vWuTrKYU5f4A0UB02XnNGemgVhDcExCgBv0yfeJIWHlpFTF9Mw==", null, false, "37cdec63-717d-4f65-9f73-9ec9a53cc620", false, "tenant1.content@test.com" },
                    { "00000005-0000-0000-0000-000000000000", 0, "99659729-fdb7-4069-b013-9c7d122ebf4a", "tenant1.member@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEPT8l8cCiYCIVuleceSfxf/pC1Rgwr0qylrvELAmGdQhz/tJnOjXCEwztxBj5BJwuQ==", null, false, "7417b553-2977-4c77-8a40-0879653d3bcb", false, "tenant1.member@test.com" },
                    { "00000006-0000-0000-0000-000000000000", 0, "986b96b4-136d-4c62-9fdf-3bf13a305e35", "tenant2.admin@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEFx9BO+BjK4reNF633I4FedRgBQDkh1/zEuWZOqmN00Xmvqvo2hQya7Xf//a1xBsMA==", null, false, "c5501591-e5cb-4a3b-a033-751dba9c568e", false, "tenant2.admin@test.com" },
                    { "00000007-0000-0000-0000-000000000000", 0, "65cb75c2-89be-4b6e-9b77-9bd7a8611366", "tenant2.content@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEOLSfPOJws6Z3Z2VlTeU4l1be1ioXoVKmoGFqx442dYyIbOBSI2IOrlT5Jk0/i3hag==", null, false, "23dd4845-5d06-474b-8994-f9c0af989b06", false, "tenant2.content@test.com" },
                    { "00000008-0000-0000-0000-000000000000", 0, "7108c75e-1b41-408b-bc51-3d4cae5036fb", "tenant2.member@test.com", true, false, null, null, null, "AQAAAAIAAYagAAAAEEGDHDN9uEczKnQSdsw2fPL0Ux63rBLJTN/vJaC6oIx0EAJXYDby9W7nwxEI6CuUvg==", null, false, "5a1a8998-de35-441c-986f-3bc23333c65f", false, "tenant2.member@test.com" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageID", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "EnumPublicPage", "IsActive", "ModifiedBy", "ModifiedDate", "MyTenantId", "SessionUserId", "TenantContinent", "TenantCountry" },
                values: new object[,]
                {
                    { 2, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 3, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 3, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 4, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 10, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 5, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 6, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 6, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 7, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 7, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 8, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 8, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 9, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 9, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 11, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 12, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 3, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 13, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 10, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 14, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 6, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 15, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 7, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 16, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 8, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 17, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 },
                    { 18, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 9, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "HostName", "IsActive", "ModifiedBy", "ModifiedDate", "MyTenantId", "Name", "SecretKey", "SessionUserId", "SmtpId", "Store", "TenantContinent", "TenantCountry", "TenantHostType" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "lifestyle-local", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0000000b-0000-0000-0000-000000000000"), "LifeStyle Store", null, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, null, 1, 0 },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "fanarts-local", true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0000000c-0000-0000-0000-000000000000"), "Fine Arts Store", null, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 2, null, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "GlobalAdmin", "00000002-0000-0000-0000-000000000000" },
                    { "User", "00000003-0000-0000-0000-000000000000" },
                    { "User", "00000004-0000-0000-0000-000000000000" },
                    { "User", "00000005-0000-0000-0000-000000000000" },
                    { "User", "00000006-0000-0000-0000-000000000000" },
                    { "User", "00000007-0000-0000-0000-000000000000" },
                    { "User", "00000008-0000-0000-0000-000000000000" }
                });

            migrationBuilder.InsertData(
                table: "TenantUsers",
                columns: new[] { "TenantUserId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "ModifiedBy", "ModifiedDate", "MyTenantId", "SessionUserId", "TenantContinent", "TenantCountry", "TenantRole", "UserId" },
                values: new object[,]
                {
                    { 1, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, "Admin", "00000003-0000-0000-0000-000000000000" },
                    { 2, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, "ContentManager", "00000004-0000-0000-0000-000000000000" },
                    { 3, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000001-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, "Member", "00000005-0000-0000-0000-000000000000" },
                    { 4, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, "Admin", "00000006-0000-0000-0000-000000000000" },
                    { 5, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, "ContentManager", "00000007-0000-0000-0000-000000000000" },
                    { 6, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000002-0000-0000-0000-000000000000"), "e02fd0e4-00fd-000a-ca30-0F00a0898ba1", null, 1, "Member", "00000008-0000-0000-0000-000000000000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdImageFiles_AdminPostID",
                table: "AdImageFiles",
                column: "AdminPostID");

            migrationBuilder.CreateIndex(
                name: "IX_AdImageFiles_MyTenantId",
                table: "AdImageFiles",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostComments_AdminPostID",
                table: "AdPostComments",
                column: "AdminPostID");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostComments_MyTenantId",
                table: "AdPostComments",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AdPosts_MyTenantId",
                table: "AdPosts",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AValues_MyTenantId",
                table: "AValues",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxMessages_MyTenantId",
                table: "EmailOutboxMessages",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSmtps_FkTenantId",
                table: "EmailSmtps",
                column: "FkTenantId",
                unique: true,
                filter: "[FkTenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionLogs_MyTenantId",
                table: "ExceptionLogs",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_MyTenantId",
                table: "Pages",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Panels_MyTenantId",
                table: "Panels",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Panels_PageID",
                table: "Panels",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MyTenantId",
                table: "Posts",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PanelID",
                table: "Posts",
                column: "PanelID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_MyTenantId",
                table: "ProductComments",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductID",
                table: "ProductComments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_MyTenantId",
                table: "ProductImageFiles",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageFiles_ProductID",
                table: "ProductImageFiles",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MyTenantId",
                table: "Products",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantInvitations_MyTenantId",
                table: "TenantInvitations",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_MyTenantId",
                table: "Tenants",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUsers_MyTenantId",
                table: "TenantUsers",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantUsers_UserId",
                table: "TenantUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_MyTenantId",
                table: "UserRefreshTokens",
                column: "MyTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdImageFiles");

            migrationBuilder.DropTable(
                name: "AdPostComments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AValues");

            migrationBuilder.DropTable(
                name: "EmailOutboxMessages");

            migrationBuilder.DropTable(
                name: "EmailSmtps");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImageFiles");

            migrationBuilder.DropTable(
                name: "TenantInvitations");

            migrationBuilder.DropTable(
                name: "TenantUsers");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "AdPosts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Panels");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
