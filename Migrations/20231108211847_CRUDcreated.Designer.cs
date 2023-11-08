﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SophosProject.PostgreSQL;

#nullable disable

namespace SophosProject.Migrations
{
    [DbContext(typeof(UniversityDBContext))]
    [Migration("20231108211847_CRUDcreated")]
    partial class CRUDcreated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SophosProject.Models.Alumno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Facultad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("Alumno");
                });

            modelBuilder.Entity("SophosProject.Models.Curso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Cupos")
                        .HasColumnType("integer");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PrerequisitoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfesorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PrerequisitoId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("SophosProject.Models.CursoAlumno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AlumnoId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CursoId")
                        .HasColumnType("uuid");

                    b.Property<int>("Estado")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("CursoId");

                    b.ToTable("CursoAlumno");
                });

            modelBuilder.Entity("SophosProject.Models.Profesor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Experiencia")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Profesor");
                });

            modelBuilder.Entity("SophosProject.Models.Curso", b =>
                {
                    b.HasOne("SophosProject.Models.Curso", "PreRequisito")
                        .WithMany("CursosSiguientes")
                        .HasForeignKey("PrerequisitoId");

                    b.HasOne("SophosProject.Models.Profesor", "Profesor")
                        .WithMany("Cursos")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreRequisito");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("SophosProject.Models.CursoAlumno", b =>
                {
                    b.HasOne("SophosProject.Models.Alumno", "Alumno")
                        .WithMany("CursoAlumnos")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophosProject.Models.Curso", "Curso")
                        .WithMany("CursoAlumnos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("SophosProject.Models.Alumno", b =>
                {
                    b.Navigation("CursoAlumnos");
                });

            modelBuilder.Entity("SophosProject.Models.Curso", b =>
                {
                    b.Navigation("CursoAlumnos");

                    b.Navigation("CursosSiguientes");
                });

            modelBuilder.Entity("SophosProject.Models.Profesor", b =>
                {
                    b.Navigation("Cursos");
                });
#pragma warning restore 612, 618
        }
    }
}
