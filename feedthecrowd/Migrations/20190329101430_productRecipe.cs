using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class productRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FkProduct = table.Column<int>(nullable: false),
                    FkProductNavigationId = table.Column<int>(nullable: true),
                    FkRecipe = table.Column<int>(nullable: false),
                    FkRecipeNavigationId = table.Column<int>(nullable: true),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeProduct_Product_FkProductNavigationId",
                        column: x => x.FkProductNavigationId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeProduct_Recipe_FkRecipeNavigationId",
                        column: x => x.FkRecipeNavigationId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_FkProductNavigationId",
                table: "RecipeProduct",
                column: "FkProductNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_FkRecipeNavigationId",
                table: "RecipeProduct",
                column: "FkRecipeNavigationId");
        }
    }
}
