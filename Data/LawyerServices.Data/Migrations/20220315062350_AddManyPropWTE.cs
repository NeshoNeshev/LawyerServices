using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class AddManyPropWTE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "WorkingTimeExceptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "WorkingTimeExceptions");
        }
    }
}
