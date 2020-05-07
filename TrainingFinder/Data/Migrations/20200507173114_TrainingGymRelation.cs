using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class TrainingGymRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Trainings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Trainings",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Gyms_GymId",
                table: "Trainings",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
