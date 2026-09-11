using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIdentity31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000003-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e688567b-42b4-4ee4-8f01-d0002e334cee", "AQAAAAIAAYagAAAAEMOyS2QF+2L15yOnjA9jZp8M+QJhdH0/fKpGF1gd+6x0Fe6iXgaiuLWqB0Nj3VlIzQ==", "6b8d2d60-26b3-4965-b4a8-da1fd22b77c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000004-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ed1a8e6-ec1b-4001-a20c-cfde693ecd2b", "AQAAAAIAAYagAAAAELxUQ0R/68QrKx2f+u1GFdLoou93VJ2StBTa8GQi2rDVZIMe1RD+MOB3kBz2Airzqg==", "bc970dec-b138-43e7-8a61-d1911874b2c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000005-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01825c7f-83d7-4313-a924-51f5065fca47", "AQAAAAIAAYagAAAAEB/seyuHyBJ3XSTFDUwIyBZXGjFNCzWxoG3KwAeCR2ryRkHoM/LlElU1RpqLO2jQvg==", "61d990c3-4cdc-4153-acbe-26e978bd2157" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000006-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1caaae72-7417-4110-a3ae-4ffcfab471d2", "AQAAAAIAAYagAAAAEIZ2YTF8yOB5gLUgqPesK2P2EZUIdZDHr0Og2YUgvFtftA8IgTKmnZFjN8hNLBBKoQ==", "23d51ad4-5e12-4c24-96ec-d00531314c83" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000007-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "22b7ec05-6e1d-47fd-b7f7-a3fad81f6b3d", "AQAAAAIAAYagAAAAEBNeA+i9X/m3qPmn1RtKkGjh53aA1BvckGFBOOMKli4eE5Ci1L5IM+oYQKNHT5OFTw==", "d895dfdc-ef90-46b4-8c12-27f51abc3b67" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000008-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e58ff810-e76f-404b-9686-1abc8e6b155f", "AQAAAAIAAYagAAAAEAjfPUQax6y535Ewsj0sp1YXNKg15W0r0WBBy2ptxdA1reyww0zq0otCI2kkmsvWjA==", "4a00716b-b27a-45f9-81d5-9b9ff470dcfe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000009-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9b920ba-c003-4723-904d-2880604fd8e5", "AQAAAAIAAYagAAAAENAs8foQg49AMTbX73V2zuYGWinPZjyuGES5gaHMJE0VeV6SnADI89PC4BRG2uJh+g==", "7a68c69d-9992-497f-9141-0895ab3268ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000a-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7982e360-5a8a-41cc-a707-7424b6ff6817", "AQAAAAIAAYagAAAAEFBwJeLAi+XquqcyIUJgXGUADDQm3A/lzq5s+tpyYUwbIFmFBJKeQtvthRmRVX7KeQ==", "845e4085-dd2e-49f1-8c0f-ad988c0fd2ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0000000b-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5074fb5-80b6-4b70-95d9-bebe863f172c", "AQAAAAIAAYagAAAAEDD8A78kFSaf8wr9Bon661smKx1xhboig+74zqgC2snvulQjTy8Zr4G6vkoZeIAXZQ==", "b214ce2c-af1a-4dd4-a68b-403335cc38f8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
