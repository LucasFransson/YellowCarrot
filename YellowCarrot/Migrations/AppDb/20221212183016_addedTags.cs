using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class addedTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Tag_TagsID",
                table: "RecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Tags_TagsID",
                table: "RecipeTag",
                column: "TagsID",
                principalTable: "Tags",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Tags_TagsID",
                table: "RecipeTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Tag_TagsID",
                table: "RecipeTag",
                column: "TagsID",
                principalTable: "Tag",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
