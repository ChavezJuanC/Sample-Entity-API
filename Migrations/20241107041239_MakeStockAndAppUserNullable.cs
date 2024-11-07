using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class MakeStockAndAppUserNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0733ab72-7e8d-4287-b6e1-52ecefc70e15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b307cbc9-c3c4-46f8-be25-ee2d6c20bb52");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d137a6b-dafb-4caf-8c29-e07be85d9ee5", null, "Admin", "ADMIN" },
                    { "e9070e25-dad0-4c34-956b-694c22ec13f4", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d137a6b-dafb-4caf-8c29-e07be85d9ee5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9070e25-dad0-4c34-956b-694c22ec13f4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0733ab72-7e8d-4287-b6e1-52ecefc70e15", null, "User", "USER" },
                    { "b307cbc9-c3c4-46f8-be25-ee2d6c20bb52", null, "Admin", "ADMIN" }
                });
        }
    }
}
