using Microsoft.EntityFrameworkCore;
using SophosProject.Models;

namespace SophosProject.PostgreSQL;

public class UniversityDBContext : DbContext
{
    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<CursoAlumno> CursoAlumnos { get; set; }
    public DbSet<Facultad> Facultades { get; set; }

    public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(alumno =>
        {
            alumno.ToTable("Alumno");
            alumno.HasKey(a => a.Id);
            alumno.Property(a => a.Id);
            alumno.Property(a => a.Nombre).IsRequired().HasMaxLength(150);
            alumno.Property(a => a.FacultadId).HasColumnName("FacultadId");
            alumno.HasOne(a => a.Facultad)
            .WithMany(f => f.Alumnos)
            .HasForeignKey(a => a.FacultadId);
        });

        modelBuilder.Entity<Profesor>(profesor =>
        {
            profesor.ToTable("Profesor");
            profesor.HasKey(p => p.Id);
            profesor.Property(p => p.Id);
            profesor.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            profesor.Property(p => p.Titulo).IsRequired().HasMaxLength(150);
            profesor.Property(p => p.Experiencia).HasDefaultValue(0);
            profesor.HasMany(p => p.Cursos)
            .WithOne(c => c.Profesor)
            .HasForeignKey(c => c.ProfesorId);
        });

        modelBuilder.Entity<Curso>(curso =>
        {
            curso.ToTable("Curso");
            curso.HasKey(c => c.Id);
            curso.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            curso.Property(c => c.Descripcion).IsRequired(false).HasMaxLength(300);
            curso.Property(c => c.Cupos).IsRequired().HasDefaultValue(0);
            curso.Property(c => c.Creditos).IsRequired().HasDefaultValue(1);
            curso.HasOne(c => c.Profesor)
            .WithMany(p => p.Cursos)
            .HasForeignKey(c => c.ProfesorId);
            curso.HasMany(c => c.CursoAlumnos)
            .WithOne(ca => ca.Curso)
            .HasForeignKey(ca => ca.CursoId);
            curso.HasOne(c => c.PreRequisito)
            .WithMany(c => c.CursosSiguientes)
            .HasForeignKey(c => c.PreRequisitoId);
        });

        modelBuilder.Entity<CursoAlumno>(ca =>
        {
            ca.ToTable("CursoAlumno");
            ca.HasKey(ca => ca.Id);

            ca.Property(ca => ca.Estado).HasDefaultValue(Estado.en_curso);
            ca.HasOne(ca => ca.Curso)
            .WithMany(c => c.CursoAlumnos)
            .HasForeignKey(ca => ca.CursoId);
            ca.HasOne(ca => ca.Alumno)
            .WithMany(a => a.CursoAlumnos)
            .HasForeignKey(ca => ca.AlumnoId);

        });

        modelBuilder.Entity<Facultad>(facultad =>
        {
            facultad.ToTable("Facultad");
            facultad.HasKey(f => f.Id);
            facultad.Property(f => f.Nombre).IsRequired().HasMaxLength(150);
            facultad.HasMany(f => f.Alumnos)
            .WithOne(a => a.Facultad)
            .HasForeignKey(a => a.FacultadId);
        });

        //Seed Data
        modelBuilder.Entity<Facultad>().HasData(
        new Facultad { Id = Guid.Parse("abfd9035-4d52-441f-b844-71bb24ee2278"), Nombre = "Ingeniería" },
        new Facultad { Id = Guid.Parse("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65"), Nombre = "Música" }
    );

        modelBuilder.Entity<Alumno>().HasData(
            new Alumno { Id = Guid.Parse("54e1e829-d7d2-4b20-b266-8dad5fb3d7b0"), Nombre = "Juan Perez", FacultadId = Guid.Parse("abfd9035-4d52-441f-b844-71bb24ee2278") },
            new Alumno { Id = Guid.Parse("32577785-96f0-4ab1-a46f-abd9048a2827"), Nombre = "Luisa Vargas", FacultadId = Guid.Parse("abfd9035-4d52-441f-b844-71bb24ee2278") },
            new Alumno { Id = Guid.Parse("e80ca0f1-fa39-4017-84ca-1f1a50bfb85d"), Nombre = "José Perdomo", FacultadId = Guid.Parse("abfd9035-4d52-441f-b844-71bb24ee2278") },
            new Alumno { Id = Guid.Parse("3c260206-daaa-4f81-b921-c721935ffa83"), Nombre = "Santiago Piñerez", FacultadId = Guid.Parse("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65") },
            new Alumno { Id = Guid.Parse("2f6401af-4446-401a-812f-5dcf0524b9f0"), Nombre = "Luis Ramos", FacultadId = Guid.Parse("f01f8fa0-e944-4b62-b49b-8c7f1d34cc65") }
        );

        modelBuilder.Entity<Profesor>().HasData(
            new Profesor { Id = Guid.Parse("3c260206-daaa-4f81-b921-c721935ffa83"), Nombre = "Julián Perea", Titulo = "Master", Experiencia = 5 },
            new Profesor { Id = Guid.Parse("a7929a9c-8c55-4f35-afe4-a80163bc8aed"), Nombre = "Marissa Arteta", Titulo = "Doctorado", Experiencia = 15 }
        );

        modelBuilder.Entity<Curso>().HasData(
            new Curso
            {
                Id = Guid.Parse("62176c30-0da4-4706-ae1f-69d758c4c683"),
                Nombre = "Calculo I",
                Descripcion = "Curso de cálculo",
                Cupos = 15,
                ProfesorId = Guid.Parse("a7929a9c-8c55-4f35-afe4-a80163bc8aed"),
                Creditos = 5
            },
            new Curso
            {
                Id = Guid.Parse("d444bcc6-077f-4d29-bbb3-88adfa812768"),
                Nombre = "Calculo II",
                Descripcion = "Curso de cálculo",
                Cupos = 15,
                PreRequisitoId = Guid.Parse("62176c30-0da4-4706-ae1f-69d758c4c683"),
                ProfesorId = Guid.Parse("a7929a9c-8c55-4f35-afe4-a80163bc8aed"),
                Creditos = 5
            },
            new Curso
            {
                Id = Guid.Parse("62c72dca-12f1-4cce-bf62-b2708b33d7e4"),
                Nombre = "Teoría de la música",
                Descripcion = "Curso de música",
                Cupos = 1,
                ProfesorId = Guid.Parse("a7929a9c-8c55-4f35-afe4-a80163bc8aed"),
                Creditos = 3
            }
        );

        modelBuilder.Entity<CursoAlumno>().HasData(
            new CursoAlumno { Id = Guid.Parse("546306fa-67a4-40e3-a7a5-ca6917eb5f00"), AlumnoId = Guid.Parse("54e1e829-d7d2-4b20-b266-8dad5fb3d7b0"), CursoId = Guid.Parse("62176c30-0da4-4706-ae1f-69d758c4c683"), Estado = Estado.en_curso },
            new CursoAlumno { Id = Guid.Parse("a4dcae21-6c18-44aa-bbba-1b0b65c38362"), AlumnoId = Guid.Parse("32577785-96f0-4ab1-a46f-abd9048a2827"), CursoId = Guid.Parse("62176c30-0da4-4706-ae1f-69d758c4c683"), Estado = Estado.en_curso },
            new CursoAlumno { Id = Guid.Parse("5b80e236-2cae-4a4d-ad97-1f426df02d44"), AlumnoId = Guid.Parse("e80ca0f1-fa39-4017-84ca-1f1a50bfb85d"), CursoId = Guid.Parse("62176c30-0da4-4706-ae1f-69d758c4c683"), Estado = Estado.cursado },
            new CursoAlumno { Id = Guid.Parse("1f55b3ff-035d-4223-a659-261038182cbc"), AlumnoId = Guid.Parse("3c260206-daaa-4f81-b921-c721935ffa83"), CursoId = Guid.Parse("62c72dca-12f1-4cce-bf62-b2708b33d7e4"), Estado = Estado.en_curso }
        );
    }
}