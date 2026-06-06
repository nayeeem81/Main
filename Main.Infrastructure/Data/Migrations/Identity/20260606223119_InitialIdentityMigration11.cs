using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2951a01-1f70-42cb-b8ad-2f2dc5b1ea89", "AQAAAAIAAYagAAAAEM4JtUPaxzRsbeqOe3tzduMfGXJbGk5p04Y2cCcsqgJmYlqldAwdVC41iL8zjG2wTw==", "150a7c5f-23f8-4b0e-bd99-023d638e3814" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6e809d0-1c68-4617-937c-992c96881d82", "AQAAAAIAAYagAAAAEGMkhuEaJxM1GFqDPfrSYGqe9dhOey52ZKivI9+I3/VHijB8qfgEt7A0FSLnlZJ+ww==", "3d119f46-3ae8-47a0-bc4e-8a7b01ab0896" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db8ac970-48f8-4ce3-ac9b-95c2763985ff", "AQAAAAIAAYagAAAAEFW6SGFh5oLa90+GMhm06uy6q7UPnbqTvdkCFhKzLr9VipSBO2gJREWBpDV1k/zmyg==", "85731297-1873-4ab5-bb32-eec7d17fd8d7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6477f01-b12d-4538-97be-e940ca7dd620", "AQAAAAIAAYagAAAAEJPtF9RE9dbOGUfyHfPCEm+dln1tXb80uz9JgVCQBJS9mLUEcJ/RYH8TDyYMHTaZtg==", "213aa3c3-d4f0-4ee3-8e1b-8c5a86f09365" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2113978d-c646-4724-a779-ecd2f387c653", "AQAAAAIAAYagAAAAEHC+8nAh/N0S2OaKJ8kdeIQuEzkOGNamHVxlsKpCFfqvCNjQJUbeXBwRKLmVfbsmGQ==", "41b8795a-1b2e-470c-8287-cd0559edf5ce" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4916bdfc-6082-4aa2-9854-dd56a0d59ede", "AQAAAAIAAYagAAAAEFKgTHNNu5iWO/OZiQH/WXN4BiQ4mhj9ktDHwx0eFVCvlxCCqbUGhj0D0wXe0ItXzg==", "3e6ca731-a77b-455e-bdf4-1e7ba7d8e111" });
        }
    }
}
