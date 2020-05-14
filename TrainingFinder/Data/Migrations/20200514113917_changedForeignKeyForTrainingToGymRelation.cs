using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class changedForeignKeyForTrainingToGymRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_GymForeignKey",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_GymForeignKey",
                table: "Trainings");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_GymId",
                table: "Trainings",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_GymId",
                table: "Trainings");

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
    }
}
