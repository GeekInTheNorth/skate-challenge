using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class AddNewSkateLogEntryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkateLogEntries",
                columns: table => new
                {
                    SkateLogEntryId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    DistanceInMiles = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Logged = table.Column<DateTime>(nullable: false),
                    StravaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkateLogEntries", x => x.SkateLogEntryId);
                    table.ForeignKey(
                        name: "FK_SkateLogEntries_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkateLogEntries_ApplicationUserId_Logged",
                table: "SkateLogEntries",
                columns: new[] { "ApplicationUserId", "Logged" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkateLogEntries");
        }
    }
}
