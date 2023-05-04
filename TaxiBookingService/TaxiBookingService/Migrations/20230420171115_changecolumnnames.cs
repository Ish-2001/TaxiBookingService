using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class changecolumnnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Seating",
                table: "VehicleDetails",
                newName: "NumberOfSeats");

            migrationBuilder.RenameColumn(
                name: "NumberPlate",
                table: "VehicleDetails",
                newName: "RegisteredNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisteredNumber",
                table: "VehicleDetails",
                newName: "NumberPlate");

            migrationBuilder.RenameColumn(
                name: "NumberOfSeats",
                table: "VehicleDetails",
                newName: "Seating");
        }
    }
}
