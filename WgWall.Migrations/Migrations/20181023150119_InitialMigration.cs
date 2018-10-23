using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WgWall.Migrations.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrontendUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Karma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrontendUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsHidden = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsHidden = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IntervalInDays = table.Column<int>(nullable: true),
                    Reward = table.Column<int>(nullable: false),
                    LastExecutionAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountableId = table.Column<int>(nullable: true),
                    ExecutedAt = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<int>(nullable: true),
                    KarmaEarned = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPurchases_FrontendUsers_AccountableId",
                        column: x => x.AccountableId,
                        principalTable: "FrontendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPurchases_Products_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskExecutions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountableId = table.Column<int>(nullable: true),
                    ExecutedAt = table.Column<DateTime>(nullable: false),
                    EntityId = table.Column<int>(nullable: true),
                    KarmaEarned = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskExecutions_FrontendUsers_AccountableId",
                        column: x => x.AccountableId,
                        principalTable: "FrontendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskExecutions_TaskTemplates_EntityId",
                        column: x => x.EntityId,
                        principalTable: "TaskTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchases_AccountableId",
                table: "ProductPurchases",
                column: "AccountableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchases_EntityId",
                table: "ProductPurchases",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutions_AccountableId",
                table: "TaskExecutions",
                column: "AccountableId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutions_EntityId",
                table: "TaskExecutions",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "ProductPurchases");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TaskExecutions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "FrontendUsers");

            migrationBuilder.DropTable(
                name: "TaskTemplates");
        }
    }
}
