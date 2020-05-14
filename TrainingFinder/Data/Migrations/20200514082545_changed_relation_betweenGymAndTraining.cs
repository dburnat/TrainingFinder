using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class changed_relation_betweenGymAndTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_GymId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Trainings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingForeignKey",
                table: "Trainings",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Trainings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
