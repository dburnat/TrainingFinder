using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class deletedRealationBetweenAppUserAndTrainingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingUsers_AppUsers_AppUserId",
                table: "TrainingUsers");

            migrationBuilder.DropIndex(
                name: "IX_TrainingUsers_AppUserId",
                table: "TrainingUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TrainingUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "TrainingUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingUsers_AppUserId",
                table: "TrainingUsers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingUsers_AppUsers_AppUserId",
                table: "TrainingUsers",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
