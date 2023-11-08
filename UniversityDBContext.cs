using Microsoft.EntityFrameworkCore;
using SophosProject.Models;

namespace SophosProject.PostgreSQL;

public class UniversityDBContext : DbContext
{
    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<CursoAlumno> CursoAlumnos { get; set; }

    public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options) { }
}