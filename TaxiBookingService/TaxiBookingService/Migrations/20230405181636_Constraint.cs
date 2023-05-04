using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class Constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
               name: "ModifiedBy",
               table: "BookingsStatus",
               type: "int",
               nullable: true,
               oldClrType: typeof(int),
               oldType: "int");


            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "BookingsStatus",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
