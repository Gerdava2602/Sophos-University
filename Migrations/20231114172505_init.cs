using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophosProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facultad",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Experiencia = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    FacultadId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumno_Facultad_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Cupos = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    PreRequisitoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfesorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Curso_PreRequisitoId",
                        column: x => x.PreRequisitoId,
                        principalTable: "Curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Curso_Profesor_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoAlumno",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CursoId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlumnoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoAlumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursoAlumno_Alumno_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoAlumno_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumno_FacultadId",
                table: "Alumno",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_PreRequisitoId",
                table: "Curso",
                column: "PreRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_ProfesorId",
                table: "Curso",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoAlumno_AlumnoId",
                table: "CursoAlumno",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoAlumno_CursoId",
                table: "CursoAlumno",
                column: "CursoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoAlumno");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Facultad");

            migrationBuilder.DropTable(
                name: "Profesor");
        }
    }
}
