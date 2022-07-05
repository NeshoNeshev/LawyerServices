using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class changeNameCourt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Court_Towns_TownId",
                table: "Court");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Court",
                table: "Court");

            migrationBuilder.RenameTable(
                name: "Court",
                newName: "Courts");

            migrationBuilder.RenameIndex(
                name: "IX_Court_TownId",
                table: "Courts",
                newName: "IX_Courts_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Court_IsDeleted",
                table: "Courts",
                newName: "IX_Courts_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courts",
                table: "Courts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Towns_TownId",
                table: "Courts",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Towns_TownId",
                table: "Courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courts",
                table: "Courts");

            migrationBuilder.RenameTable(
                name: "Courts",
                newName: "Court");

            migrationBuilder.RenameIndex(
                name: "IX_Courts_TownId",
                table: "Court",
                newName: "IX_Court_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Courts_IsDeleted",
                table: "Court",
                newName: "IX_Court_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Court",
                table: "Court",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Court_Towns_TownId",
                table: "Court",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id");
        }
    }
}
