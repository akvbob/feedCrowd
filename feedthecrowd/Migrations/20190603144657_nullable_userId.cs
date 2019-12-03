using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class nullable_userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FkUser",
                table: "Recipe",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FkUser",
                table: "Recipe",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
