using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModeId",
                table: "Bookings",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ModeId",
                table: "Bookings",
                column: "ModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_PaymentsMode_ModeId",
                table: "Bookings",
                column: "ModeId",
                principalTable: "PaymentsMode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_PaymentsMode_ModeId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ModeId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ModeId",
                table: "Bookings");
        }
    }
}
