using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailOutboxMessages_Tenants_TenantId",
                table: "EmailOutboxMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailOutboxMessages",
                table: "EmailOutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_EmailOutboxMessages_TenantId",
                table: "EmailOutboxMessages");

            migrationBuilder.RenameTable(
                name: "EmailOutboxMessages",
                newName: "EmailOutboxMessage");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "EmailOutboxMessage",
                newName: "MyTenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailOutboxMessage",
                table: "EmailOutboxMessage",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3781ba6b-7167-4c3f-9bec-b9244238f8fa", "AQAAAAIAAYagAAAAEH0c2ss0LIr5ujcOdVcKvw0VB1LaBi4ie1MxIYu6LUXPIKLE74P3jsHbPWBK2lZb3g==", "81ba4ce0-8b5f-4a87-afce-f6a777d01547" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fca685f8-abb2-47ea-8547-fd5e4d895dae", "AQAAAAIAAYagAAAAECiErRe+s3XC0yO0jcQliUKonLoi8mgvLI9OqAAKA7uJYMosh3/1keTpeKN3dcxCfg==", "955794f3-d0e1-48d6-9c34-16874a91e849" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2cc45de-c6a1-4b29-8920-dc95b9520d0a", "AQAAAAIAAYagAAAAEGkkXwo9nmGwYqrpvlb0lBbA0/vOe9Ajdknkxi3xCrjV0X8psvQfbJz+a0RafmzAyw==", "93180d02-630e-48d9-8546-df20468b3899" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af0ab5b9-1e14-4f77-97ad-2040601d7cd7", "AQAAAAIAAYagAAAAEGulYNQs+/bSRNyr7aNxPcDZzqcICxymDs7IjxBeuWbpfdYlXRd0zK5feRbblthGYg==", "033c22ed-fb7b-4643-85f1-597c986b7b20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d21b9a7-1880-4b07-9075-6ed1ca23922f", "AQAAAAIAAYagAAAAEDEe6RWGVdImUkAKCg8sdJaxpU/QTQdNjX8OPUCCWw1B3NOoypZZr6Rv/TfUoVXc8A==", "4c976eb5-cce2-4e0f-a3c5-783bffdee322" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ee28ffa-45c1-4e69-81fb-80fa26ac4779", "AQAAAAIAAYagAAAAELLmHzoAOQAAz3trU7zu29zwuqFfrVa1GNS3Ixt6Lh6ICAgVS8QV2XjLzs68tvp/vA==", "50ebea4e-91eb-4ae7-a11d-cbfdfe4e2935" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9048e26b-c7ba-4560-ac7e-375fed544bef", "AQAAAAIAAYagAAAAEDs0KiDaQRaEp3IFud92LoPwtVXvIQTe3oSb5ses+wh7o/5SUc3N7JJKe+1fL+H38A==", "c3655f1a-0ad3-4fee-8fae-42db0b8b4101" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e53edfaf-5353-4dae-9532-a2a40ee42721", "AQAAAAIAAYagAAAAENkihOadk4ssaYLQbhvBX8VVBgF9WsCb0vd2glhkB0rKB3HqQ144+s31YUKv4slcDw==", "7e3daede-9895-4495-970e-bd79c9e84e2b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30bfff84-af67-4ae4-bac0-f338e9da1a7f", "AQAAAAIAAYagAAAAECHmEpI5dwIVE01vMEYf8i97LQPdymuD7TL2lLulBxR6VHMgWUjwzN2+4XrD2cAgCA==", "44adeeba-f5b1-4221-9993-c3fafc9c06fd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailOutboxMessage",
                table: "EmailOutboxMessage");

            migrationBuilder.RenameTable(
                name: "EmailOutboxMessage",
                newName: "EmailOutboxMessages");

            migrationBuilder.RenameColumn(
                name: "MyTenantId",
                table: "EmailOutboxMessages",
                newName: "TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailOutboxMessages",
                table: "EmailOutboxMessages",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7e235a3-ad67-4231-8993-071048db5b58", "AQAAAAIAAYagAAAAELaSZd75HLJFtBXs61OjlHn5t2QX+gcLEXgZB/UmtUf/qRktvLCco3vlMne7T8WgMA==", "2966a7ec-c5fe-48bb-8394-52a7dc025080" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7b2ca18-78c9-4807-b2fe-968738cb905d", "AQAAAAIAAYagAAAAEPxRs+snHUvc+42ZES9H9xlMv9zXy3UmPsVHGdxVkUAb0KuvQ4JbTReB+5LG0hhhzA==", "2348d733-1bc0-440f-a9f9-5bc59889054c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "633d639a-7c85-4e1f-b345-702653b6ac22", "AQAAAAIAAYagAAAAELDG04pqI7zhzjzTkwNGbBqAkHPyUqxgm2ZdNRziKfX619D7piF9NziaNMexv4H0rQ==", "c72a9806-b3ca-4869-acc4-6ba1fe1234df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "795f250d-2b16-442f-a76f-f4fdadb68686", "AQAAAAIAAYagAAAAEKYzuOJvPj6N3jcTuxR+WkZcKToiFdgpR8bHEUCJsGi9ZmLY0zfi79rY1ihCN0Z2gQ==", "e67f4b85-c0a2-4381-89a3-cf9c2381eab4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5241fe55-3930-4f5b-acb3-2ec9c7276de2", "AQAAAAIAAYagAAAAELf9B5bohu33VlvouBi/DjW90PxLNffjiWhQfjlGR8ly/Z5MVyIQ1buQ/hpvMHLdww==", "2943f3f8-ade2-4b17-8029-54e2555d525e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efa4704f-afb3-4627-ac20-4194a4209e16", "AQAAAAIAAYagAAAAEOxGggJ9wWosKHVVEoELiMrA96YqCOmNfBm6hPuReOfjf0M5r58J+QQxjOVfYXksYg==", "2fb86047-e1aa-4b83-a90b-fb20f32d1c87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a25021ca-79f8-432e-a440-775160d9bae1", "AQAAAAIAAYagAAAAEOOi1J5t604de513V7zDVs07BmkN6brGvWw9k5KbSjDnO55bzwiX+d0b1wF91Z6S8g==", "89f2d250-3d74-4d9c-bd7f-ca1caf172d77" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9038892e-bb93-45f5-b687-7dc944ab19fa", "AQAAAAIAAYagAAAAEBzAAkV+XTQyrUdn2lBq25B5JglK0qqHwVXhtEvvzxSPI17CcwSvskNYwyTzCCfogA==", "603f4e87-1413-4c45-aa19-2b034628b57e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fee6fd6c-89a4-490d-8328-dd04476583c8", "AQAAAAIAAYagAAAAEP58OGxCJkXrMoZj/bFUmCRBeJrneZNU2rt3XxkU3PtmTJMnM+Kv4v0HYAyXhO/DOg==", "500bef51-0171-4cda-ba4a-4e4fdaf753fa" });

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxMessages_TenantId",
                table: "EmailOutboxMessages",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailOutboxMessages_Tenants_TenantId",
                table: "EmailOutboxMessages",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
