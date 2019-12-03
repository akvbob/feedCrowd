using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class eventrecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Event_EventId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_EventId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Recipe");

            migrationBuilder.AddColumn<int>(
                name: "FkUser",
                table: "Event",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FkUser",
                table: "Event");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Recipe",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_EventId",
                table: "Recipe",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Event_EventId",
                table: "Recipe",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
