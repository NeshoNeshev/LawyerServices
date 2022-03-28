using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class updateCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FixedCost",
                table: "Companies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NoWinNoFee",
                table: "Companies",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedCost",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NoWinNoFee",
                table: "Companies");
        }
    }
}
