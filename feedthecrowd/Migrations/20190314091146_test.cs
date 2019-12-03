using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedTheCrowd.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FkUser = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Servings = table.Column<int>(nullable: false),
                    FkUserNavigationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_User_FkUserNavigationId",
                        column: x => x.FkUserNavigationId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TextField = table.Column<string>(nullable: true),
                    FkUser = table.Column<int>(nullable: false),
                    FkRecipe = table.Column<int>(nullable: false),
                    FkRecipeNavigationId = table.Column<int>(nullable: true),
                    FkUserNavigationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Recipe_FkRecipeNavigationId",
                        column: x => x.FkRecipeNavigationId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_User_FkUserNavigationId",
                        column: x => x.FkUserNavigationId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    FkRecipe = table.Column<int>(nullable: false),
                    FkRecipeNavigationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Recipe_FkRecipeNavigationId",
                        column: x => x.FkRecipeNavigationId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<double>(nullable: false),
                    FkRecipe = table.Column<int>(nullable: false),
                    FkProduct = table.Column<int>(nullable: false),
                    FkProductNavigationId = table.Column<int>(nullable: true),
                    FkRecipeNavigationId = table.Column<int>(nullable: true)
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
                name: "IX_Comment_FkRecipeNavigationId",
                table: "Comment",
                column: "FkRecipeNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FkUserNavigationId",
                table: "Comment",
                column: "FkUserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FkRecipeNavigationId",
                table: "Product",
                column: "FkRecipeNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_FkUserNavigationId",
                table: "Recipe",
                column: "FkUserNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_FkProductNavigationId",
                table: "RecipeProduct",
                column: "FkProductNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeProduct_FkRecipeNavigationId",
                table: "RecipeProduct",
                column: "FkRecipeNavigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "RecipeProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
