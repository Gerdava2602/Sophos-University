using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SophosProject.Models;

public class Profesor
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Titulo { get; set; }
    public int Experiencia { get; set; }
    [JsonIgnore]
    public virtual ICollection<Curso> Cursos { get; } = new List<Curso>();
}