using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.Data.Migrations
{
    public partial class CityInfoDBInitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointOfInteres");

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "PointsOfInteres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsOfInteres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointsOfInteres_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointsOfInteres_CityId",
                table: "PointsOfInteres",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointsOfInteres");

            migrationBuilder.CreateTable(
                name: "PointOfInteres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointOfInteres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointOfInteres_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "The one with that big park", "New York" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "The one with the cathedral that was never really ..", "Antwerp" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "The one with that big tower", "Paris" });

            migrationBuilder.InsertData(
                table: "PointOfInteres",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 1, 1, "The most visited urban park in the United States.", "Central Park" });

            migrationBuilder.InsertData(
                table: "PointOfInteres",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 2, 1, "A 102-story skyscraper located in Midtown Manhattan.", "Empire State Building" });

            migrationBuilder.InsertData(
                table: "PointOfInteres",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 3, 2, "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.", "Cathedral" });

            migrationBuilder.InsertData(
                table: "PointOfInteres",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 4, 2, "The the finest example of railway architecture in Belgium.", "Antwerp Central Station" });

            migrationBuilder.InsertData(
                table: "PointOfInteres",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 5, 3, "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.", "Eiffel Tower" });

            migrationBuilder.InsertData(
                table: "PointOfInteres",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 6, 3, "The world's largest museum.", "The Louvre" });

            migrationBuilder.CreateIndex(
                name: "IX_PointOfInteres_CityId",
                table: "PointOfInteres",
                column: "CityId");
        }
    }
}
