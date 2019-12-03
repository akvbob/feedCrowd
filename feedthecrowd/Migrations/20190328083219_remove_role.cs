using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class remove_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }
    }
}
