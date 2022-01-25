using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class changeWirkingTimeException : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimeExceptions_AspNetUsers_UserId",
                table: "WorkingTimeExceptions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkingTimeExceptions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimeExceptions_AspNetUsers_UserId",
                table: "WorkingTimeExceptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimeExceptions_AspNetUsers_UserId",
                table: "WorkingTimeExceptions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkingTimeExceptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimeExceptions_AspNetUsers_UserId",
                table: "WorkingTimeExceptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
