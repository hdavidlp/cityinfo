using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.Data.Migrations
{
    public partial class CitiInfoDB2AddPointOfInterestDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PointsOfInteres",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PointsOfInteres");
        }
    }
}
