using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class ChangeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Profesion",
                table: "Requests",
                newName: "Profession");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Requests",
                newName: "Names");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Profession",
                table: "Requests",
                newName: "Profesion");

            migrationBuilder.RenameColumn(
                name: "Names",
                table: "Requests",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
