using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class changedRelationGymToTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_TrainingForeignKey",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_TrainingForeignKey",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "TrainingForeignKey",
                table: "Trainings");

            migrationBuilder.AddColumn<int>(
                name: "GymForeignKey",
                table: "Trainings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_GymForeignKey",
                table: "Trainings",
                column: "GymForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Gyms_GymForeignKey",
                table: "Trainings",
                column: "GymForeignKey",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_GymForeignKey",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_GymForeignKey",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "GymForeignKey",
                table: "Trainings");

            migrationBuilder.AddColumn<int>(
                name: "TrainingForeignKey",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingForeignKey",
                table: "Trainings",
                column: "TrainingForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Gyms_TrainingForeignKey",
                table: "Trainings",
                column: "TrainingForeignKey",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
