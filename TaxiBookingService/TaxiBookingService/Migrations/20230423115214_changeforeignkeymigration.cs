using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class changeforeignkeymigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LocationId",
                table: "Drivers",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Locations_LocationId",
                table: "Drivers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Locations_LocationId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_LocationId",
                table: "Drivers");
        }
    }
}
