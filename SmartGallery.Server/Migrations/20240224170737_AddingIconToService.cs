using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGallery.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingIconToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29be0170-fe61-40e7-8b9b-2f5d057d39f0");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "fas fa-server");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8953d2c8-457d-46c8-bb9e-ecd8b980aaf6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f2df951-22db-462e-808a-d225222d5e5e", "96749580-cdd2-4d56-a251-8739661c211a", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "635333b9-dcbc-4a7e-ba51-da0b05d94e7e", "AQAAAAIAAYagAAAAEDLF7SsjTK7lZQR6i8PGmam7qB8OoYkAaP6R2uRul+OP/l7N4RXAV9IBGHjgA2tGjw==", "e571b823-2224-4c1d-9fe7-bf8c4a7e3208" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f2df951-22db-462e-808a-d225222d5e5e");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Services");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ce5333bf-8133-4ebf-a2c6-a3747cf14cbc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29be0170-fe61-40e7-8b9b-2f5d057d39f0", "17af6e0e-3c5b-4d55-a39c-d4cf8a192f0f", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2f5eae5-7570-4681-965e-d969e0370268", "AQAAAAIAAYagAAAAEPNpUH1Kdoz8nX/NpVmaQWS8F1etmMcSEuPtJWj7/Mkk+A5+Wyw8PA/gJct7PmY6rg==", "cea01be5-ab05-45d5-a8c5-332af3fb72c8" });
        }
    }
}
