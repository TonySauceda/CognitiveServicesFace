using Microsoft.EntityFrameworkCore.Migrations;

namespace CognitiveServicesFace.Migrations
{
    public partial class ActualizacionModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "Emotions",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Score",
                table: "Emotions",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
