using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophosProject.Migrations
{
    /// <inheritdoc />
    public partial class creditos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Creditos",
                table: "Curso",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creditos",
                table: "Curso");
        }
    }
}
