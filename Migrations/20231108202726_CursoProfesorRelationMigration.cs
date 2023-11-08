using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophosProject.Migrations
{
    /// <inheritdoc />
    public partial class CursoProfesorRelationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoProfesor");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfesorId",
                table: "Curso",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Curso_ProfesorId",
                table: "Curso",
                column: "ProfesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Profesor_ProfesorId",
                table: "Curso",
                column: "ProfesorId",
                principalTable: "Profesor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Profesor_ProfesorId",
                table: "Curso");

            migrationBuilder.DropIndex(
                name: "IX_Curso_ProfesorId",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "ProfesorId",
                table: "Curso");

            migrationBuilder.CreateTable(
                name: "CursoProfesor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CursoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfesorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoProfesor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoProfesor_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoProfesor_Profesor_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfesor_CursoId",
                table: "CursoProfesor",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoProfesor_ProfesorId",
                table: "CursoProfesor",
                column: "ProfesorId");
        }
    }
}
