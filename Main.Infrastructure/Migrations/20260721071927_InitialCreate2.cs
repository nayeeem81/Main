using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "417eca7d-2c41-4b46-957f-ed14868ea9c1", "AQAAAAIAAYagAAAAEDMcMeWlDk5uyLRvqaNwhvPN5N4jYh0YWK4KgIq/QHHLbud7LyqF5fuGqO73NtRC4w==", "8882a8fe-a78e-483f-bc61-43e16147dddf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6beb66a5-2adc-47ea-9048-3c0ab6f23a91", "AQAAAAIAAYagAAAAEE3it847wYlIuxrk39svRHskQLhHYO/+g/sU83gijKQfjQXGvEqcm4wy5gzFiR3NXw==", "5d7da80f-6772-4105-8df8-bf998052f5f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83011f8c-3242-453f-b055-a2796592c816", "AQAAAAIAAYagAAAAEA1cPVZ+3iFDpS3NaNEpN/V7fUb4K5p1LuZRV9D0x0mXH473uEwUnK9XE6uEiKHmnA==", "88187e6d-b6cd-4e3d-b40b-47713c64e10b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a839861c-3ef7-484e-a1e8-7348fe6e0c19", "AQAAAAIAAYagAAAAEAUqfjq8M54jOx75s8scB3btkKCixj6ZnCb5k1btNDkRuKmK7Z56Y2C0hwb1rOm/MQ==", "2226be74-50b9-418c-af27-b55933a39c24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6418a5c-f344-4edb-becd-808080e92dd0", "AQAAAAIAAYagAAAAECdlj4fZcFILU/hTip6qgNiXNcUCRDSecrqdi6MF4Kjv2abyyeP2hN+nWh/rOR7fXQ==", "3afae34e-b02d-47f3-8692-2589964d05a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92d17598-4b75-4793-aa8e-aef0afb1a779", "AQAAAAIAAYagAAAAEIkYKocWGXQqBoaNpC+V5YxCc00L3+468LR5lOunhukFS35OQkK3G0tXrAFGCy4Hug==", "8eec66fb-9ee6-49cd-9714-d287308f7229" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5034e2db-787b-4bc2-a826-45a86dca4f1d", "AQAAAAIAAYagAAAAEGpR8ac6Y6bfhm+35PCRHsrfhBgAaS6AdpeCj5N94CgUdq3z6jYRv+xfM5lk2asgxQ==", "09e32fc2-9230-4018-ae56-c98723b01c6e" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                column: "HostName",
                value: "lifestyles");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                column: "HostName",
                value: "finearts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000002-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0860f796-6a62-4b00-9383-a31ae956f6ae", "AQAAAAIAAYagAAAAEMla/2PdDqa4uFuZBKBQJVp2GhRUv1PmgcNnSCPwVg7Vi4p0jCuKQlngVBmT2wmXHQ==", "38605e23-aaa1-4c6b-a83c-d5c33cb69d02" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7af91095-afec-4622-9eba-5fb00fcdcbbb", "AQAAAAIAAYagAAAAEPpsv0fLvcvh7I2vLgPxs/pRzp+WzJ7tYxNsKsN01OC1FTYIo6wUrjRwVmpBHyRNtw==", "e2cead54-cde5-49bf-922f-88a87aa85950" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae65f95b-bcc0-444f-9e9c-ec33e2d4b3e6", "AQAAAAIAAYagAAAAEMkTFa8+xo/NXMV7vWuTrKYU5f4A0UB02XnNGemgVhDcExCgBv0yfeJIWHlpFTF9Mw==", "37cdec63-717d-4f65-9f73-9ec9a53cc620" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99659729-fdb7-4069-b013-9c7d122ebf4a", "AQAAAAIAAYagAAAAEPT8l8cCiYCIVuleceSfxf/pC1Rgwr0qylrvELAmGdQhz/tJnOjXCEwztxBj5BJwuQ==", "7417b553-2977-4c77-8a40-0879653d3bcb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "986b96b4-136d-4c62-9fdf-3bf13a305e35", "AQAAAAIAAYagAAAAEFx9BO+BjK4reNF633I4FedRgBQDkh1/zEuWZOqmN00Xmvqvo2hQya7Xf//a1xBsMA==", "c5501591-e5cb-4a3b-a033-751dba9c568e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65cb75c2-89be-4b6e-9b77-9bd7a8611366", "AQAAAAIAAYagAAAAEOLSfPOJws6Z3Z2VlTeU4l1be1ioXoVKmoGFqx442dYyIbOBSI2IOrlT5Jk0/i3hag==", "23dd4845-5d06-474b-8994-f9c0af989b06" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7108c75e-1b41-408b-bc51-3d4cae5036fb", "AQAAAAIAAYagAAAAEEGDHDN9uEczKnQSdsw2fPL0Ux63rBLJTN/vJaC6oIx0EAJXYDby9W7nwxEI6CuUvg==", "5a1a8998-de35-441c-986f-3bc23333c65f" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"),
                column: "HostName",
                value: "lifestyle-local");

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "TenantId",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"),
                column: "HostName",
                value: "fanarts-local");
        }
    }
}
