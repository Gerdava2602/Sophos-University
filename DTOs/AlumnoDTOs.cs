using System.ComponentModel.DataAnnotations;

namespace SophosProject.DTOs;

public record CreateAlumno([Required][MaxLength(150)] string Nombre, [Required] Guid FacultadId);

public record UpdateAlumno([Required][MaxLength(150)] string? Nombre, [Required] Guid? FacultadId);

public record ListAlumno(
    string Nombre,
    string? Facultad,
    int Creditos
);