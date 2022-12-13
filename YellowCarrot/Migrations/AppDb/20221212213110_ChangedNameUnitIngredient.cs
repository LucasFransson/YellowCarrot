using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ChangedNameUnitIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Measurement",
                table: "Ingredients",
                newName: "Unit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Ingredients",
                newName: "Measurement");
        }
    }
}
