using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class changenameproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneVerfication",
                table: "LawFirms",
                newName: "PhoneVerification");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LawFirms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "LawFirms");

            migrationBuilder.RenameColumn(
                name: "PhoneVerification",
                table: "LawFirms",
                newName: "PhoneVerfication");
        }
    }
}
