using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartGallery.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bfbd7b4-01ab-43d2-bf29-8c7f92161ca8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc94ed69-ee8e-48bb-bd5f-df8862038bd1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d2090c16-6a08-4572-beaf-8cca1ebc9e45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "ce5333bf-8133-4ebf-a2c6-a3747cf14cbc", "Admin", "ADMIN" },
                    { "29be0170-fe61-40e7-8b9b-2f5d057d39f0", "17af6e0e-3c5b-4d55-a39c-d4cf8a192f0f", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, null, "a2f5eae5-7570-4681-965e-d969e0370268", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEPNpUH1Kdoz8nX/NpVmaQWS8F1etmMcSEuPtJWj7/Mkk+A5+Wyw8PA/gJct7PmY6rg==", "01018004723", true, "cea01be5-ab05-45d5-a8c5-332af3fb72c8", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29be0170-fe61-40e7-8b9b-2f5d057d39f0");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bfbd7b4-01ab-43d2-bf29-8c7f92161ca8", "2b161a8b-e24a-4b02-8fb3-cb447c8a09f0", "Admin", "ADMIN" },
                    { "dc94ed69-ee8e-48bb-bd5f-df8862038bd1", "ae38ac48-ad22-4940-be3c-c4a03ae8de68", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d2090c16-6a08-4572-beaf-8cca1ebc9e45", 0, null, "9890cdd3-8202-4737-b9cb-e0eeea3e9017", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEBbpq07gc5zf4V23iSLtrqE73QZIxI33udycS599pGiWLqthK2CBERX8jNWJt0YODA==", "01018004723", true, "c9f358fe-ec9c-4468-9846-dd6de4e55edc", false, "admin@gmail.com" });
        }
    }
}
