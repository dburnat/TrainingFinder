using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class AddLatAndLonToGym : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Gyms",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Gyms",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Gyms");
        }
    }
}
