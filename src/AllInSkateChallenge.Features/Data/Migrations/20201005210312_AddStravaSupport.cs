using Microsoft.EntityFrameworkCore.Migrations;

namespace AllInSkateChallenge.Data.Migrations
{
    public partial class AddStravaSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalProfileImage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStravaAccount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalProfileImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsStravaAccount",
                table: "AspNetUsers");
        }
    }
}
