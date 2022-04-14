using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class addisownerincompanyfasdasasdYu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Qualifications",
                table: "Companies",
                newName: "HeaderText");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Companies",
                newName: "AboutText");

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "YearFirstAdmitted",
                table: "Companies",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "YearFirstAdmitted",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "HeaderText",
                table: "Companies",
                newName: "Qualifications");

            migrationBuilder.RenameColumn(
                name: "AboutText",
                table: "Companies",
                newName: "Experience");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
