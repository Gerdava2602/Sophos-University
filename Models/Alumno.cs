using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosProject.Models;

[Table("Alumno")]
public class Alumno
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(150)]
    public string Nombre { get; set; }
    [Required]
    public string Facultad { get; set; }
    public ICollection<CursoAlumno> CursoAlumnos { get; set; }

}