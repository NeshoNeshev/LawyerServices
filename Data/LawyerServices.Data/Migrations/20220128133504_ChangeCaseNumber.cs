using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class ChangeCaseNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "WorkingTimeExceptions",
                newName: "CaseNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseNumber",
                table: "WorkingTimeExceptions",
                newName: "Description");
        }
    }
}
