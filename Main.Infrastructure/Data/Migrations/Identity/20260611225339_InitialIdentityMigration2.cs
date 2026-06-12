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
                values: new object[] { "462eb5d3-2378-4548-8041-8a0293e8fa17", "AQAAAAIAAYagAAAAEB83+192RB9pePED8xB0b2W0rMiLRXIuGlWf2JP5/+aNCJzkDNFhqhUQ73Mpyl8FZA==", "c918fb0e-a069-48dc-81a6-c8b3e9948fdc" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d24fd79c-016d-4b2a-9066-17416d8d171b", "AQAAAAIAAYagAAAAEO2HvYTU+XpJwTiP7L57sFbuXIeXqWhEUE+Cp/5dPTAk95lwSdSZg16fEF5ZEfunaQ==", "9f97e3b8-78f3-4098-8c26-df14fe58bd76" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da24a767-28a0-4a47-a7e3-88be0dfc52c2", "AQAAAAIAAYagAAAAEMXqWrUz1eiQRS1mC01hqGj/Evx4YBwISeXW6YFaIbT9++cJWSdUkSVYkFZntj/E9w==", "83641148-3f99-4ef1-9983-e301ba9e231b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6faae5a-003d-40a3-b4b1-6655f922d214", "AQAAAAIAAYagAAAAEFDWO9KavkqXaUEpJqSNssOC3KY5cVLO+L39AG5KCqfj2KhMG6aoKQdguurkAI4GPQ==", "be09c3bc-5c86-4c10-9adc-a86c6b2d1f59" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "776f1ffb-4b44-4423-a7fd-ae13720fa936", "AQAAAAIAAYagAAAAEMawxsLiFDb95QXSgLUv2met5kWnaddYY0TSkhmN2wZh5kd0biZ/nVC5Lnl37nNnvA==", "e90d00db-9b60-4db0-aa94-b69af97ecb7e" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2bbbeda-0019-4a3e-adc8-aa0953eb3719", "AQAAAAIAAYagAAAAEGX9dj9o19X411393V1gFHXjZugOshngk+5P09LU8JCTDW+EB7DEggd6zp7S9ay2uA==", "f3891a38-71a8-4fe4-a90b-ce96a81c0851" });
        }
    }
}
