using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedUserIDandLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipesID = table.Column<int>(type: "int", nullable: false),
                    TagsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipesID, x.TagsID });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipesID",
                        column: x => x.RecipesID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tag_TagsID",
                        column: x => x.TagsID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagsID",
                table: "RecipeTag",
                column: "TagsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Recipes");
        }
    }
}
