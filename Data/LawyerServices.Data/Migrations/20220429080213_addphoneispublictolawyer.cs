using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class addphoneispublictolawyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "LawFirms",
                newName: "PhoneNumbers");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublicPhoneNuber",
                table: "LawFirms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublicPhoneNuber",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumbers",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublicPhoneNuber",
                table: "LawFirms");

            migrationBuilder.DropColumn(
                name: "IsPublicPhoneNuber",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PhoneNumbers",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "PhoneNumbers",
                table: "LawFirms",
                newName: "PhoneNumber");
        }
    }
}
