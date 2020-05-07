using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class addedNewProperty_IsAddedByUser_ToGymModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAddedByUser",
                table: "Gyms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAddedByUser",
                table: "Gyms");
        }
    }
}
