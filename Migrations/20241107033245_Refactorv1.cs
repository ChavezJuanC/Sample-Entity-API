using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Refactorv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "757f0d4a-9b15-4065-ac97-0ba206e2e345");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d601b685-015e-400c-b025-c1c290757699");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0733ab72-7e8d-4287-b6e1-52ecefc70e15", null, "User", "USER" },
                    { "b307cbc9-c3c4-46f8-be25-ee2d6c20bb52", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "757f0d4a-9b15-4065-ac97-0ba206e2e345", null, "User", "USER" },
                    { "d601b685-015e-400c-b025-c1c290757699", null, "Admin", "ADMIN" }
                });
        }
    }
}
