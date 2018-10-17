using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WgWall.Migrations.Migrations
{
    public partial class AddingTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedById = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IntervalInDays = table.Column<int>(nullable: true),
                    LastActivationAt = table.Column<DateTime>(nullable: true),
                    Hide = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTemplates_FrontendUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "FrontendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedById = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ActivatedAt = table.Column<DateTime>(nullable: false),
                    TaskTemplateId = table.Column<int>(nullable: false),
                    DoneById = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_FrontendUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "FrontendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_FrontendUsers_DoneById",
                        column: x => x.DoneById,
                        principalTable: "FrontendUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTemplates_TaskTemplateId",
                        column: x => x.TaskTemplateId,
                        principalTable: "TaskTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DoneById",
                table: "Tasks",
                column: "DoneById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskTemplateId",
                table: "Tasks",
                column: "TaskTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplates_CreatedById",
                table: "TaskTemplates",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskTemplates");
        }
    }
}
