using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class addedDtoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyDimension_Users_UserId",
                table: "BodyDimension");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyDimension",
                table: "BodyDimension");

            migrationBuilder.RenameTable(
                name: "BodyDimension",
                newName: "BodyDimensions");

            migrationBuilder.RenameIndex(
                name: "IX_BodyDimension_UserId",
                table: "BodyDimensions",
                newName: "IX_BodyDimensions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyDimensions",
                table: "BodyDimensions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyDimensions_Users_UserId",
                table: "BodyDimensions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyDimensions_Users_UserId",
                table: "BodyDimensions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyDimensions",
                table: "BodyDimensions");

            migrationBuilder.RenameTable(
                name: "BodyDimensions",
                newName: "BodyDimension");

            migrationBuilder.RenameIndex(
                name: "IX_BodyDimensions_UserId",
                table: "BodyDimension",
                newName: "IX_BodyDimension_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyDimension",
                table: "BodyDimension",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyDimension_Users_UserId",
                table: "BodyDimension",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
