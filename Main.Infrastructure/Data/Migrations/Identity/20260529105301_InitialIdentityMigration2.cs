using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f01570f9-308b-4206-81a4-227bcc80256c", "AQAAAAIAAYagAAAAEKxkenZHweHdQMR6B6BlC5uEtU2XL9mvlPHhJIbJMnTIlpgLRpxRXSYkUce2wjqQPg==", "3c655ec3-e2e0-478a-913d-6e94c8962770" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62a7b545-2c49-450b-9672-19bd35686c98", "AQAAAAIAAYagAAAAEG8mb7l+SJHfjUacFbGx9rSRjOVLv9ScHQA+bPpYTs6DQDg40tRWAp2SKfTrgOv83g==", "a8da0306-3826-406e-9f1a-02341cf9c696" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4743135f-1eca-4176-8a5c-66e599f54a7a", "AQAAAAIAAYagAAAAECBNs6UwuCmxL2glWDmjx4YlmLmIxOzD92DNnf/ja8nv4K9ncUgVJd2zoKcXX2Kg8w==", "7c73f5f4-4716-4122-8403-1883b3a0cd11" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab53ba5f-9a44-4e50-a1d3-039b9e31d2a3", "AQAAAAIAAYagAAAAEEU0+ntqyUVCZxjF/SKEB9NzFIKEeu3CwuOVs9I2IZ0kTDEAftGnnIwELqBAb3AVwA==", "f9ef67c3-14ea-4f70-bea5-4cd07c26a779" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8546df1e-899b-49e3-8a6f-7645561c0cfe", "AQAAAAIAAYagAAAAEErrrmIVTnPMa+99BuGmKaKEcY+XpIo6VtvM/hiOM20t9a81PKisRJfc11UaiNfC9Q==", "df51dbf4-101f-4711-ad5f-ea446b1eddf9" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f3f493f-20d2-43dc-9642-8603a4d18778", "AQAAAAIAAYagAAAAEFSxqesQlLkahnpLN67OaCoRx+MEC9oKCNpR1YyNwkJierngE3CsJzu/g+LCCGxZ+A==", "6ac2a94e-23fa-4e79-bd3a-634311502c1c" });
        }
    }
}
