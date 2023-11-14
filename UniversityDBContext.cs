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
    }
}