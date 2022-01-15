using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class ChangeRequestTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rsquests",
                table: "Rsquests");

            migrationBuilder.RenameTable(
                name: "Rsquests",
                newName: "Requests");

            migrationBuilder.RenameIndex(
                name: "IX_Rsquests_IsDeleted",
                table: "Requests",
                newName: "IX_Requests_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Rsquests");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_IsDeleted",
                table: "Rsquests",
                newName: "IX_Rsquests_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rsquests",
                table: "Rsquests",
                column: "Id");
        }
    }
}
