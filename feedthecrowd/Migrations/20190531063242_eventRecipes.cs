using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class eventRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventRecipe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FkEvent = table.Column<int>(nullable: false),
                    FkRecipe = table.Column<int>(nullable: false),
                    FkRecipeNavigationId = table.Column<int>(nullable: true),
                    FkEventNavigationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRecipe_Event_FkEventNavigationId",
                        column: x => x.FkEventNavigationId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventRecipe_Recipe_FkRecipeNavigationId",
                        column: x => x.FkRecipeNavigationId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventRecipe_FkEventNavigationId",
                table: "EventRecipe",
                column: "FkEventNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRecipe_FkRecipeNavigationId",
                table: "EventRecipe",
                column: "FkRecipeNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventRecipe");
        }
    }
}
