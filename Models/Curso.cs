using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosProject.Models;

[Table("Curso")]
public class Curso
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    [DefaultValue(0)]
    public int Cupos { get; set; }
    // One-to-Many relationship with itself
    [ForeignKey("PrerequisitoId")]
    public Guid? PrerequisitoId { get; set; }
    public Curso PreRequisito { get; set; }
    public ICollection<Curso> CursosSiguientes { get; set; }

    //One to many relationship with profesor
    [ForeignKey("ProfesorId")]
    public Guid ProfesorId { get; set; }
    public Profesor Profesor { get; set; }

    // Many-to-Many relationship
    public ICollection<CursoAlumno> CursoAlumnos { get; set; }
}