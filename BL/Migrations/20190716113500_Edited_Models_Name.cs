using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class Edited_Models_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeterReading",
                table: "WaterMeters",
                newName: "MeterData");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Houses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeterData",
                table: "WaterMeters",
                newName: "MeterReading");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Houses",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
