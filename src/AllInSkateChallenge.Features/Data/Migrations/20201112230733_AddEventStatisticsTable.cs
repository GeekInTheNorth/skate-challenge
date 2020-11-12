using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class AddEventStatisticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventStatistics",
                columns: table => new
                {
                    EventStatisticId = table.Column<Guid>(nullable: false),
                    NumberOfSkaters = table.Column<int>(nullable: false),
                    CumulativeMiles = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStatistics", x => x.EventStatisticId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventStatistics");
        }
    }
}
