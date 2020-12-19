using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingFinder.Data.Migrations
{
    public partial class addedBodyDimensionModelAndRelationWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyDimension",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Neck = table.Column<string>(nullable: true),
                    Shoulder = table.Column<string>(nullable: true),
                    Chest = table.Column<string>(nullable: true),
                    Wrist = table.Column<string>(nullable: true),
                    Bicep = table.Column<string>(nullable: true),
                    Thigh = table.Column<string>(nullable: true),
                    Calf = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyDimension", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyDimension_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyDimension_UserId",
                table: "BodyDimension",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyDimension");
        }
    }
}
