using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class newcolumnsmigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Complaint",
                table: "DriverComplaints",
                type: "varchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "DriverComplaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DriverComplaints_BookingId",
                table: "DriverComplaints",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverComplaints_Bookings_BookingId",
                table: "DriverComplaints",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverComplaints_Bookings_BookingId",
                table: "DriverComplaints");

            migrationBuilder.DropIndex(
                name: "IX_DriverComplaints_BookingId",
                table: "DriverComplaints");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "DriverComplaints");

            migrationBuilder.AlterColumn<string>(
                name: "Complaint",
                table: "DriverComplaints",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldMaxLength: 70);
        }
    }
}
