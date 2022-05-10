using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class changetoboolonarea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BindingName",
                table: "AreasOfActivities");

            migrationBuilder.AddColumn<bool>(
                name: "IsActiv",
                table: "AreasOfActivities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActiv",
                table: "AreasOfActivities");

            migrationBuilder.AddColumn<string>(
                name: "BindingName",
                table: "AreasOfActivities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
