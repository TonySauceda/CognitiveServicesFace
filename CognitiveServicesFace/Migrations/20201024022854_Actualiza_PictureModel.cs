using Microsoft.EntityFrameworkCore.Migrations;

namespace CognitiveServicesFace.Migrations
{
    public partial class Actualiza_PictureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Pictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Pictures",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Pictures");
        }
    }
}
