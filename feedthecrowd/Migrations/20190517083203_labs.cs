using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class labs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Recipe",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    PeopleCount = table.Column<int>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Event_EventId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_EventId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Recipe");
        }
    }
}
