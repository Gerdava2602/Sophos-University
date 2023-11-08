using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SophosProject.Models;

[Table("Profesor")]
public class Profesor
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string Titulo { get; set; }
    [DefaultValue(0)]
    public int Experiencia { get; set; }
    public ICollection<Curso> Cursos { get; set; }
}