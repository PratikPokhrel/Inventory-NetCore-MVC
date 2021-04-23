using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.DAL.Migrations
{
    public partial class addedimageurlinproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "dbo",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "dbo",
                table: "Product");
        }
    }
}
