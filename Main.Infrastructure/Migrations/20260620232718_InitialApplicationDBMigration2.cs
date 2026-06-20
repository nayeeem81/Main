using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialApplicationDBMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "ProductImageFiles");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "Panels");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "AValue");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "AdPosts");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "AdPostComments");

            migrationBuilder.DropColumn(
                name: "HostCompanyName",
                table: "AdImageFiles");

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1aab437-90e4-4824-95a7-67463514ddec", "AQAAAAIAAYagAAAAEOMJXMujdrLC+r3kbLELt+P+kw1Qlio+VseW4FhhKE1mS26b0/4wGbFeDGdQLDh/3Q==", "aab94157-51a8-4c7b-9ca6-25d87bd5f524" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-da10-0d00a2228ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33885a1a-b48a-4853-8c54-fa6aad7fe8b1", "AQAAAAIAAYagAAAAEAxUXGcxy47UuLPQDZy98UQZrSVgPm9IPyM4pqmfdd6zr8LWEWtpNKikwBYv0q0b2g==", "1409d254-f4c3-4f2e-845d-cfd118287f79" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49f2575f-1b2b-47c4-8043-788892033f94", "AQAAAAIAAYagAAAAEIukdmRxR3plqqMyk3kSYJSaq6CGigj7YfmPujshYDadBd4JJEzZrlajwduM/GH6Qw==", "fcc1cb69-f046-4082-9987-07dfd5a7479a" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0e4-55fd-095a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d4d67c2-4400-448d-9cb0-8a99ce93ea79", "AQAAAAIAAYagAAAAEKzd+DhKaamQwyxHhnY83NQ1wZE3oiMe6we0keFh0qN1Doin0LAkwn0jrmeTdAoEUg==", "c2cc0957-f84a-478c-92eb-3d1342c15897" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "ProductImageFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "Panels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "AValue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "AdPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "AdPostComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostCompanyName",
                table: "AdImageFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97a662c1-aff7-48a8-81a4-1c5ebbfca850", "AQAAAAIAAYagAAAAEP0J9Skjr4ht06d28ZQezgKHFhQ+ETaoNcQ3W9JaX2qHX/BN2ernC0+tobpCjMEetw==", "7224ebc4-3aa8-43b8-8c4c-69540589246c" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-da10-0d00a2228ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26c50294-1a79-4209-9a82-d9d8b8c038c7", "AQAAAAIAAYagAAAAEOHofetYdE8Dxecq6FGj+WXfDdNpsAEYsFuRcJpEzSDnYu2n4Mapx4DWgHII9k4fdQ==", "66ad697b-51ec-43f1-9250-f40c37a88e41" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26015cf6-a8b1-44e7-8f05-c79a2af6bc30", "AQAAAAIAAYagAAAAEDrbEbcsTPhgT3vjJ0XxP7IvJfnz1chTEF6XaWldlK6aWvXxf78UYlk85mZtLZIiaA==", "d63faf03-9d67-4b2c-aeca-677fb6f21c8c" });

            migrationBuilder.UpdateData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "e03fd0e4-55fd-095a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48611df6-7743-42a8-a6f5-f3f5956f9c2d", "AQAAAAIAAYagAAAAEIdeBlaaVpLyEhwpAdW5xtnCHX5hQi4OMSzYnaNN7kuvTHjG8EA6xBT/7Sur/3jK+w==", "74926dac-064a-4d1a-bf91-e56df4d000fb" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 1,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 2,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 3,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 4,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 5,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 6,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 7,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 8,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 9,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 10,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 11,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 12,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 13,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 14,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 15,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 16,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 17,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 18,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 19,
                column: "HostCompanyName",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "PageID",
                keyValue: 20,
                column: "HostCompanyName",
                value: 2);
        }
    }
}
