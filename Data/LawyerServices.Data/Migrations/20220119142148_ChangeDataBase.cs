using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class ChangeDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimes_Companies_CompanyId",
                table: "WorkingTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkingTimes_CompanyId",
                table: "WorkingTimes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkingTimes");

            migrationBuilder.AddColumn<string>(
                name: "WorkingTimeId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_WorkingTimeId",
                table: "Companies",
                column: "WorkingTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_WorkingTimes_WorkingTimeId",
                table: "Companies",
                column: "WorkingTimeId",
                principalTable: "WorkingTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_WorkingTimes_WorkingTimeId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_WorkingTimeId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WorkingTimeId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "WorkingTimes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_CompanyId",
                table: "WorkingTimes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimes_Companies_CompanyId",
                table: "WorkingTimes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
