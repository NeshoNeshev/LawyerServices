using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class addNotShowUpinWte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotShowUp",
                table: "AspNetUsers",
                newName: "NotShowUpCount");

            migrationBuilder.AddColumn<bool>(
                name: "NotShowUp",
                table: "WorkingTimeExceptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotShowUp",
                table: "WorkingTimeExceptions");

            migrationBuilder.RenameColumn(
                name: "NotShowUpCount",
                table: "AspNetUsers",
                newName: "NotShowUp");
        }
    }
}
