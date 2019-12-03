using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class removePass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                nullable: true);
        }
    }
}
