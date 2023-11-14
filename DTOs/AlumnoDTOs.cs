using System.ComponentModel.DataAnnotations;
using SophosProject.Models;

namespace SophosProject.DTOs;

public record CreateAlumno([Required][MaxLength(150)] string Nombre, [Required] Guid FacultadId);

public record UpdateAlumno([Required][MaxLength(150)] string? Nombre, [Required] Guid? FacultadId);

public record ListAlumno(
    Guid Id,
    string Nombre,
    string? Facultad,
    int Creditos
);

public record GetAlumno(
    Guid Id,
    string Nombre,
    int CreditosInscritos,
    List<Curso> Matriculados,
    List<Curso> Cursados
);