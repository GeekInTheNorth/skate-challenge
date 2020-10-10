using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class AddLoggingForStravaWebhooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StravaIntegrationLogs",
                columns: table => new
                {
                    StravaIntegrationLogId = table.Column<Guid>(nullable: false),
                    Recieved = table.Column<DateTime>(nullable: false),
                    QueryString = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StravaIntegrationLogs", x => x.StravaIntegrationLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StravaIntegrationLogs");
        }
    }
}
