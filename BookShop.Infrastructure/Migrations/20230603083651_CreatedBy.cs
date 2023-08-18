using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "BookShops",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookShops_CreatedById",
                table: "BookShops",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_BookShops_AspNetUsers_CreatedById",
                table: "BookShops",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookShops_AspNetUsers_CreatedById",
                table: "BookShops");

            migrationBuilder.DropIndex(
                name: "IX_BookShops_CreatedById",
                table: "BookShops");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "BookShops");
        }
    }
}
