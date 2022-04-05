using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class changecolumincompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_LawFirms_LawFirmId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "LawFirmId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_LawFirms_LawFirmId",
                table: "Companies",
                column: "LawFirmId",
                principalTable: "LawFirms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_LawFirms_LawFirmId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "LawFirmId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_LawFirms_LawFirmId",
                table: "Companies",
                column: "LawFirmId",
                principalTable: "LawFirms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
