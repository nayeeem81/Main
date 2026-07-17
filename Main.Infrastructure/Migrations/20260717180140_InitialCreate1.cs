using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "TenantUsers");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "TenantInvitations");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "ProductImageFiles");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "Panels");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "ExceptionLogs");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "EmailSmtps");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "EmailOutboxMessages");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "AValues");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "AdPosts");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "AdPostComments");

            migrationBuilder.DropColumn(
                name: "GlobalUserRole",
                table: "AdImageFiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "761ff7e2-299b-4992-8a8e-742ea1a808b2", "AQAAAAIAAYagAAAAEDhr+FM7kguXl+FbY5pNdSkQAyJ4D9dC+vRTH3I26oywvoE00Wa1eEi0kJDoxBNMAQ==", "32083fc8-9f39-4b07-ab63-400775d09a14" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "857567cb-13ec-494f-b3f8-f59fefe2abdc", "AQAAAAIAAYagAAAAEE88ZjPxAmR2krVcVqtMytNALnEW80cWMAl2QbWtEqCUrZP/i9GOFx3UmU6fMQgsfg==", "6b198697-796d-4f15-b3b5-b83b05b9a48e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62fb6f8d-3262-443c-b8f0-f2f41bb2c506", "AQAAAAIAAYagAAAAEFms2GWoGaF3HrOBy2Fb/4uvnHxYVfna/UAPbPSUaJc50/tOxVAcDqZsLFXZqt9ilw==", "dc6a396f-f615-4485-b6a3-12256322e352" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "050ddaa7-f1d2-4bee-b2d6-3f559cc0c18c", "AQAAAAIAAYagAAAAEE4IF5wkJlXaSx7yCtouwBO/3gPVj1Y4aHkl6efQ1NAyZl64/+hloer/xV4223Qm7w==", "bb2cde0b-c115-4976-8c4c-905d5ecfaa25" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab9f52ed-bd4a-4f4d-96e9-e920f36412b3", "AQAAAAIAAYagAAAAEJ9VCOddrkOqczZtc41dWsaVvWAIhFjk7ojCvka2zNPlLuIq9zfBJrnYbGOv2l7eSg==", "725966f6-7791-4eb5-ba86-22d02bbb52d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1de4faba-def5-4067-ab63-a6da60db1250", "AQAAAAIAAYagAAAAEPobpLb4XDEynv4dpCTvbY9OYqyjtVKzh3kVFBF/iIQacjtQcddHnhToN4b1Btof3g==", "8b225025-c8d7-4302-9de0-315c9c52ea33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1880eafb-eccc-46d2-9f46-473948c88056", "AQAAAAIAAYagAAAAECF9fpS3/XA75USVv7Z7iBAU1/6ONZ+VpzpiNUB4maYKndV93pSFYrKxCx7Mh3Ldlg==", "e95d8d16-e290-40ce-8f11-ec99c9c06d77" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "UserRefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "TenantUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "TenantInvitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "ProductImageFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "ProductComments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "Panels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "ExceptionLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "EmailSmtps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "EmailOutboxMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "AValues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "AdPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "AdPostComments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalUserRole",
                table: "AdImageFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64b51844-7cc8-4648-a0aa-c5bb2f2af9cc", "AQAAAAIAAYagAAAAEP31kx0d4wcVT0MwkKvRX8lQPXhYE9SkblCxrHjiuNDndrNQLodsaeDZkdwaA3PjLg==", "f9959fa3-63a4-4722-8c52-423384d4dd9c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32f6046c-cef3-4aff-9a89-58f597027490", "AQAAAAIAAYagAAAAEH1MT523SFWfTjkuP+hzqxZTc73oqNEaP4LrjyW3Ji5k2uNrV3d7EW9rcALgDPAsjw==", "159a6a8e-1640-4fee-b23a-7d1d5944497e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a5e500e-1a96-45c2-bcd7-703382612dd0", "AQAAAAIAAYagAAAAEOhNS0XJbi0KHCoiaZY7kd9TdHG7CS/XOtqg429MuJBi/tQviHcuvHbQeYtVhD+qAQ==", "bcc36d83-6eea-4ab2-b601-186204300878" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f20cbc54-419f-4e72-b204-56ac4679810b", "AQAAAAIAAYagAAAAED8drPZmFstA8dFgq9Q2PtqNAb8fZRYZK0LE1Yr2Qa0mkJFsvtdKFUVMX3qQHTjNfw==", "29c5ca5f-460b-4ace-936e-a98259845813" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d172ecf9-f44b-421b-bdf5-b0dca01859c2", "AQAAAAIAAYagAAAAELU24CCHGvkCQjJtlnewsBf0t2YbfGOW4smJX0bMCva10CgNmWMfGXZInxMO3rHlag==", "52912a00-42de-4dd1-82ab-804add1f1dd2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54aa9358-7cfb-482b-b6df-0bee005fed18", "AQAAAAIAAYagAAAAEJsYSTVqHCW+o/MZBb/jSBF/2JByHZlTPcv0XgU/vMyDQ8OND4xbvuWaKvLycF0c7Q==", "6dd0b487-e195-4973-becf-f3b1447a7414" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4611adc0-ecef-47f9-b7e8-cf3ee90148aa", "AQAAAAIAAYagAAAAEAQU8sSMQGIbsfRYT6h1MNbjKWFUcWSqrUu/eHZVYgfHBGKHtp2XIvlvf+1CMTsG2A==", "ca8947ca-a125-4705-8ec6-b338f4a98006" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 2,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 3,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 4,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 5,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 6,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 7,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 8,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 9,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 11,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 12,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 13,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 14,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 15,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 16,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 17,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 18,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "TenantUsers",
                keyColumn: "TenantUserId",
                keyValue: 1,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "TenantUsers",
                keyColumn: "TenantUserId",
                keyValue: 2,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "TenantUsers",
                keyColumn: "TenantUserId",
                keyValue: 3,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "TenantUsers",
                keyColumn: "TenantUserId",
                keyValue: 4,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "TenantUsers",
                keyColumn: "TenantUserId",
                keyValue: 5,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "TenantUsers",
                keyColumn: "TenantUserId",
                keyValue: 6,
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                column: "GlobalUserRole",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                column: "GlobalUserRole",
                value: null);
        }
    }
}
