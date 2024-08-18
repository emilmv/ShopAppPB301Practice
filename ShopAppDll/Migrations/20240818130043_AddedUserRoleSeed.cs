using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopAppDll.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "044290b5-2db4-45d5-a798-c37c3f148092", null, "Admin", "ADMIN" },
                    { "f865ff70-1ecf-4f12-8ae8-83ad587829c8", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "44b92d0d-6957-4393-984a-a0c0c7cbd804", 0, "0f573f1c-4bbe-4d60-bf34-7ab0c7e36852", "admin@example.com", true, null, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEOofgTlMIOAxTxgnA5K2lXsnsk1DZkjJk4CAD8ifzxGNXi11aVoWOsU2lCEaiwd1GQ==", null, false, "2bee17bf-deb1-46b6-a00d-7dc7cd14bf7f", false, "Admin" },
                    { "e8cae89b-9de6-4d9e-9e23-a4c38f63649f", 0, "82a77ffd-89a5-4c60-8409-70f35f1b3d0b", "user@example.com", true, null, false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAEIkBasp/wijp1WEDJ1EgslODS1cSFap3wQusEXFUvAXsFpbQsHD3Zsjd/sEnH4uYfw==", null, false, "9287b4f7-94c1-48b1-8647-aa9da5d4e255", false, "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "044290b5-2db4-45d5-a798-c37c3f148092");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f865ff70-1ecf-4f12-8ae8-83ad587829c8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "44b92d0d-6957-4393-984a-a0c0c7cbd804");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e8cae89b-9de6-4d9e-9e23-a4c38f63649f");
        }
    }
}
