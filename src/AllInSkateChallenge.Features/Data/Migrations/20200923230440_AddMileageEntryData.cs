using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class AddMileageEntryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MileageEntries",
                columns: table => new
                {
                    MileageEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    Logged = table.Column<DateTime>(nullable: false),
                    Miles = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Kilometres = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ExerciseUrl = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileageEntries", x => x.MileageEntryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MileageEntries_UserId_MileageEntryId",
                table: "MileageEntries",
                columns: new[] { "UserId", "MileageEntryId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MileageEntries");
        }
    }
}
