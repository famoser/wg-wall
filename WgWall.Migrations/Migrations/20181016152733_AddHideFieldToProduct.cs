using Microsoft.EntityFrameworkCore.Migrations;

namespace WgWall.Migrations.Migrations
{
    public partial class AddHideFieldToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Products");
        }
    }
}
