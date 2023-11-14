using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SophosProject.Models;

public class Facultad
{
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Alumno> Alumnos { get; } = new List<Alumno>();
}