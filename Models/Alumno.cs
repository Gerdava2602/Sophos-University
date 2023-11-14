using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SophosProject.Models;

public class Alumno
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public Guid FacultadId { get; set; }
    [JsonIgnore]
    public virtual Facultad Facultad { get; set; }
    [JsonIgnore]
    public virtual ICollection<CursoAlumno> CursoAlumnos { get; } = new List<CursoAlumno>();

}