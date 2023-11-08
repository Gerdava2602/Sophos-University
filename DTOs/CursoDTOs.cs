using System.ComponentModel.DataAnnotations;

namespace SophosProject.DTOs;

public record CreateCurso([Required]
    string Nombre,
    string Descripcion,
    int? Cupos,
    Guid? PrerequisitoId,
    Guid ProfesorId);

public record UpdateCurso([Required]
    string? Nombre,
    string? Descripcion,
    int? Cupos,
    Guid? PrerequisitoId,
    Guid? ProfesorId);