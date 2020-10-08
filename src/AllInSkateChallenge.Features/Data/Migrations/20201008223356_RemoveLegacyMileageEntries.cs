using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class RemoveLegacyMileageEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MileageEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MileageEntries",
                columns: table => new
                {
                    MileageEntryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Kilometres = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Miles = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
    }
}
