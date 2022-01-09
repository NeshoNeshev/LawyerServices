using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class sdadadadasdadsas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Languages",
                table: "Rsquests");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "Rsquests");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Rsquests");

            migrationBuilder.RenameColumn(
                name: "Names",
                table: "Rsquests",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Rsquests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Rsquests");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Rsquests",
                newName: "Names");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "Rsquests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "Rsquests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Rsquests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
