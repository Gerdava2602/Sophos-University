using System.ComponentModel.DataAnnotations;

namespace SophosProject.DTOs;

public record CreateAlumno([Required][MaxLength(150)] string Nombre, [Required] string Facultad);

public record UpdateAlumno([Required][MaxLength(150)] string? Nombre, [Required] string? Facultad);