using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class AddWorkingTimeExceptionIsApprovedAndIsRequested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "WorkingTimeExceptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequested",
                table: "WorkingTimeExceptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "WorkingTimeExceptions");

            migrationBuilder.DropColumn(
                name: "IsRequested",
                table: "WorkingTimeExceptions");
        }
    }
}
