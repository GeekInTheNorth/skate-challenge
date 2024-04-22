using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllInSkateChallenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSkaterTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Target",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 12);

            migrationBuilder.AddColumn<int>(
                name: "Team",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Target",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 12,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
