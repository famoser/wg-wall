using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WgWall.Migrations.Migrations
{
    public partial class AddPlates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountableId = table.Column<int>(nullable: true),
                    ValidityDate = table.Column<DateTime>(nullable: false),
                    DinnerState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plates_FrontendUsers_AccountableId",
                        column: x => x.AccountableId,
                        principalTable: "FrontendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plates_AccountableId",
                table: "Plates",
                column: "AccountableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plates");
        }
    }
}
