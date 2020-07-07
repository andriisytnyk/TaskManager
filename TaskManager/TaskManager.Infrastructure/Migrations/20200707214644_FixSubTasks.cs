using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Infrastructure.Migrations
{
    public partial class FixSubTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_GlobalTasks_ParentTask",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_PlannedTasks_ParentTask",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_ParentTask",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "ParentTask",
                table: "SubTasks");

            migrationBuilder.AddColumn<int>(
                name: "ParentGlobalTask",
                table: "SubTasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentPlannedTask",
                table: "SubTasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_ParentGlobalTask",
                table: "SubTasks",
                column: "ParentGlobalTask");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_ParentPlannedTask",
                table: "SubTasks",
                column: "ParentPlannedTask");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_GlobalTasks_ParentGlobalTask",
                table: "SubTasks",
                column: "ParentGlobalTask",
                principalTable: "GlobalTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_PlannedTasks_ParentPlannedTask",
                table: "SubTasks",
                column: "ParentPlannedTask",
                principalTable: "PlannedTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_GlobalTasks_ParentGlobalTask",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_PlannedTasks_ParentPlannedTask",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_ParentGlobalTask",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_ParentPlannedTask",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "ParentGlobalTask",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "ParentPlannedTask",
                table: "SubTasks");

            migrationBuilder.AddColumn<int>(
                name: "ParentTask",
                table: "SubTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_ParentTask",
                table: "SubTasks",
                column: "ParentTask");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_GlobalTasks_ParentTask",
                table: "SubTasks",
                column: "ParentTask",
                principalTable: "GlobalTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_PlannedTasks_ParentTask",
                table: "SubTasks",
                column: "ParentTask",
                principalTable: "PlannedTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
