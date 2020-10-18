using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class AssignEventsToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StravaEvents",
                columns: table => new
                {
                    StravaEventId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    StravaActivityId = table.Column<string>(nullable: true),
                    Imported = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StravaEvents", x => x.StravaEventId);
                    table.ForeignKey(
                        name: "FK_StravaEvents_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StravaEvents_ApplicationUserId_StravaActivityId",
                table: "StravaEvents",
                columns: new[] { "ApplicationUserId", "StravaActivityId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StravaEvents");
        }
    }
}
