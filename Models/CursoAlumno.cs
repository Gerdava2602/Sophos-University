using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosProject.Models;

[Table("CursoAlumno")]
public class CursoAlumno
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("CursoId")]
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; }
    [ForeignKey("AlumnoId")]
    public Guid AlumnoId { get; set; }
    public Alumno Alumno { get; set; }
    [DefaultValue(Estado.en_curso)]
    public Estado Estado { get; set; }
}

public enum Estado
{
    cursado,
    en_curso,
}