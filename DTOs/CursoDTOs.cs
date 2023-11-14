using System.ComponentModel.DataAnnotations;

namespace SophosProject.DTOs;

public record CreateCurso([Required]
    string Nombre,
    string Descripcion,
    int? Cupos,
    int? Creditos,
    Guid? PrerequisitoId,
    Guid ProfesorId);

public record UpdateCurso(
    string? Nombre,
    string? Descripcion,
    int? Cupos,
    int? Creditos,
    Guid? PrerequisitoId,
    Guid? ProfesorId);

public record ListCurso(
    string Nombre,
    string? NombrePrerrequisito,
    int Creditos,
    int CuposDisponibles
);