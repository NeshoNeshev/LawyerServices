using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class addstopaccountincompanyr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StopAccount",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StopAccount",
                table: "Companies");
        }
    }
}
