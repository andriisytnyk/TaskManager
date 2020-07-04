using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Infrastructure.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false, defaultValue: "New task"),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    FinishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalTasks", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "PlannedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false, defaultValue: "New task"),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    FinishDateTime = table.Column<DateTime>(nullable: false),
                    Estimation = table.Column<TimeSpan>(nullable: false),
                    Requirement = table.Column<bool>(nullable: false, defaultValue: true),
                    Frequency = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedTasks", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "SubTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false, defaultValue: "New task"),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    ParentTask = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTasks", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_SubTasks_GlobalTasks_ParentTask",
                        column: x => x.ParentTask,
                        principalTable: "GlobalTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubTasks_PlannedTasks_ParentTask",
                        column: x => x.ParentTask,
                        principalTable: "PlannedTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_ParentTask",
                table: "SubTasks",
                column: "ParentTask");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubTasks");

            migrationBuilder.DropTable(
                name: "GlobalTasks");

            migrationBuilder.DropTable(
                name: "PlannedTasks");
        }
    }
}
