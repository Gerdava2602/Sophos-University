using System.ComponentModel.DataAnnotations;
using SophosProject.Models;

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
    Guid Id,
    string Nombre,
    string? NombrePrerrequisito,
    int Creditos,
    int CuposDisponibles
);

public record GetCurso(
    Guid Id,
    string Nombre,
    string? Profesor,
    int Creditos,
    List<Alumno> Alumnos
);