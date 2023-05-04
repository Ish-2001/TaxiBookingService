using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBookingService.Migrations
{
    public partial class RemoveRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_VehicleCategories_VehicleCategoryId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_VehicleDetails_VehicleDetailsId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_VehicleDetailsId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "VehicleDetailsId",
                table: "Drivers");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleDetailId",
                table: "Drivers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleCategoryId",
                table: "Drivers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_VehicleDetailId",
                table: "Drivers",
                column: "VehicleDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_VehicleCategories_VehicleCategoryId",
                table: "Drivers",
                column: "VehicleCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_VehicleDetails_VehicleDetailId",
                table: "Drivers",
                column: "VehicleDetailId",
                principalTable: "VehicleDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_VehicleCategories_VehicleCategoryId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_VehicleDetails_VehicleDetailId",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_VehicleDetailId",
                table: "Drivers");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleDetailId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleCategoryId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleDetailsId",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_VehicleDetailsId",
                table: "Drivers",
                column: "VehicleDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_VehicleCategories_VehicleCategoryId",
                table: "Drivers",
                column: "VehicleCategoryId",
                principalTable: "VehicleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_VehicleDetails_VehicleDetailsId",
                table: "Drivers",
                column: "VehicleDetailsId",
                principalTable: "VehicleDetails",
                principalColumn: "Id");
        }
    }
}
