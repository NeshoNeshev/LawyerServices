using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerServices.Data.Migrations
{
    public partial class ewew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingTimes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActiv = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTimes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkingTimeDays",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StarFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    IsDayOff = table.Column<bool>(type: "bit", nullable: false),
                    WorkingTimeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimeDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTimeDays_WorkingTimes_WorkingTimeId",
                        column: x => x.WorkingTimeId,
                        principalTable: "WorkingTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkingTimeExceptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StarFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkingTimeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimeExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTimeExceptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkingTimeExceptions_WorkingTimes_WorkingTimeId",
                        column: x => x.WorkingTimeId,
                        principalTable: "WorkingTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimeDays_IsDeleted",
                table: "WorkingTimeDays",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimeDays_WorkingTimeId",
                table: "WorkingTimeDays",
                column: "WorkingTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimeExceptions_IsDeleted",
                table: "WorkingTimeExceptions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimeExceptions_UserId",
                table: "WorkingTimeExceptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimeExceptions_WorkingTimeId",
                table: "WorkingTimeExceptions",
                column: "WorkingTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_CompanyId",
                table: "WorkingTimes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_IsDeleted",
                table: "WorkingTimes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingTimeDays");

            migrationBuilder.DropTable(
                name: "WorkingTimeExceptions");

            migrationBuilder.DropTable(
                name: "WorkingTimes");
        }
    }
}
