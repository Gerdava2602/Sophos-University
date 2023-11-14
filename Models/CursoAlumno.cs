using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosProject.Models;

public class CursoAlumno
{
    public Guid Id { get; set; }
    public Guid CursoId { get; set; }
    public virtual Curso Curso { get; set; }
    public Guid AlumnoId { get; set; }
    public virtual Alumno Alumno { get; set; }
    public Estado Estado { get; set; }
}

public enum Estado
{
    cursado,
    en_curso,
}