using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class endstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "Event",
                newName: "EventStartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEndDate",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEndDate",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "EventStartDate",
                table: "Event",
                newName: "EventDate");
        }
    }
}
