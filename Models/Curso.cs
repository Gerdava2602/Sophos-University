using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SophosProject.Models;

public class Curso
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Cupos { get; set; }
    public int Creditos { get; set; }
    // One-to-Many relationship with itself
    public Guid? PreRequisitoId { get; set; }
    public virtual Curso? PreRequisito { get; set; }
    [JsonIgnore]
    public ICollection<Curso> CursosSiguientes { get; set; }
    public Guid ProfesorId { get; set; }
    //One to many relationship with profesor
    public virtual Profesor Profesor { get; set; }

    // Many-to-Many relationship
    [JsonIgnore]
    public virtual ICollection<CursoAlumno> CursoAlumnos { get; } = new List<CursoAlumno>();
}