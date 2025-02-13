using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurants.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class OwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE Restaurants " +
                "SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fce7af9-2279-4a32-a719-e4a4f3cfd154", null, "Admin", "ADMIN" },
                    { "57e3ba63-4d22-4e02-8b92-9be98dda6b95", null, "User", "USER" },
                    { "62c3d2b0-e827-4c68-9df8-acee864ab4b6", null, "Owner", "OWNER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurants",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurants");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fce7af9-2279-4a32-a719-e4a4f3cfd154");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57e3ba63-4d22-4e02-8b92-9be98dda6b95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62c3d2b0-e827-4c68-9df8-acee864ab4b6");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Restaurants");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" },
                    { "3", null, "Owner", "OWNER" }
                });
        }
    }
}
