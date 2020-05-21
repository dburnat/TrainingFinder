using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class changedshadowProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Trainings");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Trainings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Trainings");

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
