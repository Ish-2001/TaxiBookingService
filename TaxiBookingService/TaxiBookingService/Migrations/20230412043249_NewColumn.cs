using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class NewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_ModifiedBy",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ModifiedBy",
                table: "Locations",
                column: "ModifiedBy",
                unique: true,
                filter: "[ModifiedBy] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_ModifiedBy",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Drivers");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ModifiedBy",
                table: "Locations",
                column: "ModifiedBy");
        }
    }
}
