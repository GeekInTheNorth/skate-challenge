using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class ElevationAndSpeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageSpeedInMph",
                table: "SkateLogEntries",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "SkateLogEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ElevationGainInFeet",
                table: "SkateLogEntries",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HighestElevationInFeet",
                table: "SkateLogEntries",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LowestElevationInFeet",
                table: "SkateLogEntries",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TopSpeedInMph",
                table: "SkateLogEntries",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageSpeedInMph",
                table: "SkateLogEntries");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "SkateLogEntries");

            migrationBuilder.DropColumn(
                name: "ElevationGainInFeet",
                table: "SkateLogEntries");

            migrationBuilder.DropColumn(
                name: "HighestElevationInFeet",
                table: "SkateLogEntries");

            migrationBuilder.DropColumn(
                name: "LowestElevationInFeet",
                table: "SkateLogEntries");

            migrationBuilder.DropColumn(
                name: "TopSpeedInMph",
                table: "SkateLogEntries");
        }
    }
}
