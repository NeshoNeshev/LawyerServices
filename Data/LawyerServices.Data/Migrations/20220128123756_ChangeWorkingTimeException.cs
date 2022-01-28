using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class ChangeWorkingTimeException : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentType",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Court",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoreInformation",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentType",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "Court",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "MoreInformation",
                table: "WorkingTimeExceptions");
        }
    }
}
