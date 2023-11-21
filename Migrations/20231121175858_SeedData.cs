using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SophosProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Facultad",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("abfd9035-4d52-441f-b844-71bb24ee2278"), "Ingeniería" },
                    { new Guid("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65"), "Música" }
                });

            migrationBuilder.InsertData(
                table: "Profesor",
                columns: new[] { "Id", "Experiencia", "Nombre", "Titulo" },
                values: new object[,]
                {
                    { new Guid("3c260206-daaa-4f81-b921-c721935ffa83"), 5, "Julián Perea", "Master" },
                    { new Guid("a7929a9c-8c55-4f35-afe4-a80163bc8aed"), 15, "Marissa Arteta", "Doctorado" }
                });

            migrationBuilder.InsertData(
                table: "Alumno",
                columns: new[] { "Id", "FacultadId", "Nombre" },
                values: new object[,]
                {
                    { new Guid("2f6401af-4446-401a-812f-5dcf0524b9f0"), new Guid("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65"), "Luis Ramos" },
                    { new Guid("32577785-96f0-4ab1-a46f-abd9048a2827"), new Guid("abfd9035-4d52-441f-b844-71bb24ee2278"), "Luisa Vargas" },
                    { new Guid("3c260206-daaa-4f81-b921-c721935ffa83"), new Guid("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65"), "Santiago Piñerez" },
                    { new Guid("54e1e829-d7d2-4b20-b266-8dad5fb3d7b0"), new Guid("abfd9035-4d52-441f-b844-71bb24ee2278"), "Juan Perez" },
                    { new Guid("e80ca0f1-fa39-4017-84ca-1f1a50bfb85d"), new Guid("abfd9035-4d52-441f-b844-71bb24ee2278"), "José Perdomo" }
                });

            migrationBuilder.InsertData(
                table: "Curso",
                columns: new[] { "Id", "Creditos", "Cupos", "Descripcion", "Nombre", "PreRequisitoId", "ProfesorId" },
                values: new object[,]
                {
                    { new Guid("62176c30-0da4-4706-ae1f-69d758c4c683"), 5, 15, "Curso de cálculo", "Calculo I", null, new Guid("a7929a9c-8c55-4f35-afe4-a80163bc8aed") },
                    { new Guid("62c72dca-12f1-4cce-bf62-b2708b33d7e4"), 3, 1, "Curso de música", "Teoría de la música", null, new Guid("a7929a9c-8c55-4f35-afe4-a80163bc8aed") },
                    { new Guid("d444bcc6-077f-4d29-bbb3-88adfa812768"), 5, 15, "Curso de cálculo", "Calculo II", new Guid("62176c30-0da4-4706-ae1f-69d758c4c683"), new Guid("a7929a9c-8c55-4f35-afe4-a80163bc8aed") }
                });

            migrationBuilder.InsertData(
                table: "CursoAlumno",
                columns: new[] { "Id", "AlumnoId", "CursoId", "Estado" },
                values: new object[,]
                {
                    { new Guid("1f55b3ff-035d-4223-a659-261038182cbc"), new Guid("3c260206-daaa-4f81-b921-c721935ffa83"), new Guid("62c72dca-12f1-4cce-bf62-b2708b33d7e4"), 1 },
                    { new Guid("546306fa-67a4-40e3-a7a5-ca6917eb5f00"), new Guid("54e1e829-d7d2-4b20-b266-8dad5fb3d7b0"), new Guid("62176c30-0da4-4706-ae1f-69d758c4c683"), 1 }
                });

            migrationBuilder.InsertData(
                table: "CursoAlumno",
                columns: new[] { "Id", "AlumnoId", "CursoId" },
                values: new object[] { new Guid("5b80e236-2cae-4a4d-ad97-1f426df02d44"), new Guid("e80ca0f1-fa39-4017-84ca-1f1a50bfb85d"), new Guid("62176c30-0da4-4706-ae1f-69d758c4c683") });

            migrationBuilder.InsertData(
                table: "CursoAlumno",
                columns: new[] { "Id", "AlumnoId", "CursoId", "Estado" },
                values: new object[] { new Guid("a4dcae21-6c18-44aa-bbba-1b0b65c38362"), new Guid("32577785-96f0-4ab1-a46f-abd9048a2827"), new Guid("62176c30-0da4-4706-ae1f-69d758c4c683"), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alumno",
                keyColumn: "Id",
                keyValue: new Guid("2f6401af-4446-401a-812f-5dcf0524b9f0"));

            migrationBuilder.DeleteData(
                table: "Curso",
                keyColumn: "Id",
                keyValue: new Guid("d444bcc6-077f-4d29-bbb3-88adfa812768"));

            migrationBuilder.DeleteData(
                table: "CursoAlumno",
                keyColumn: "Id",
                keyValue: new Guid("1f55b3ff-035d-4223-a659-261038182cbc"));

            migrationBuilder.DeleteData(
                table: "CursoAlumno",
                keyColumn: "Id",
                keyValue: new Guid("546306fa-67a4-40e3-a7a5-ca6917eb5f00"));

            migrationBuilder.DeleteData(
                table: "CursoAlumno",
                keyColumn: "Id",
                keyValue: new Guid("5b80e236-2cae-4a4d-ad97-1f426df02d44"));

            migrationBuilder.DeleteData(
                table: "CursoAlumno",
                keyColumn: "Id",
                keyValue: new Guid("a4dcae21-6c18-44aa-bbba-1b0b65c38362"));

            migrationBuilder.DeleteData(
                table: "Profesor",
                keyColumn: "Id",
                keyValue: new Guid("3c260206-daaa-4f81-b921-c721935ffa83"));

            migrationBuilder.DeleteData(
                table: "Alumno",
                keyColumn: "Id",
                keyValue: new Guid("32577785-96f0-4ab1-a46f-abd9048a2827"));

            migrationBuilder.DeleteData(
                table: "Alumno",
                keyColumn: "Id",
                keyValue: new Guid("3c260206-daaa-4f81-b921-c721935ffa83"));

            migrationBuilder.DeleteData(
                table: "Alumno",
                keyColumn: "Id",
                keyValue: new Guid("54e1e829-d7d2-4b20-b266-8dad5fb3d7b0"));

            migrationBuilder.DeleteData(
                table: "Alumno",
                keyColumn: "Id",
                keyValue: new Guid("e80ca0f1-fa39-4017-84ca-1f1a50bfb85d"));

            migrationBuilder.DeleteData(
                table: "Curso",
                keyColumn: "Id",
                keyValue: new Guid("62176c30-0da4-4706-ae1f-69d758c4c683"));

            migrationBuilder.DeleteData(
                table: "Curso",
                keyColumn: "Id",
                keyValue: new Guid("62c72dca-12f1-4cce-bf62-b2708b33d7e4"));

            migrationBuilder.DeleteData(
                table: "Facultad",
                keyColumn: "Id",
                keyValue: new Guid("abfd9035-4d52-441f-b844-71bb24ee2278"));

            migrationBuilder.DeleteData(
                table: "Facultad",
                keyColumn: "Id",
                keyValue: new Guid("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65"));

            migrationBuilder.DeleteData(
                table: "Profesor",
                keyColumn: "Id",
                keyValue: new Guid("a7929a9c-8c55-4f35-afe4-a80163bc8aed"));
        }
    }
}
