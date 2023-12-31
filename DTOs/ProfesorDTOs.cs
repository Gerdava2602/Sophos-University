using System.ComponentModel.DataAnnotations;

namespace SophosProject.DTOs;

public record CreateProfesor(string Nombre, string Titulo, int Experiencia);

public record UpdateProfesor(string? Nombre, string? Titulo, int? Experiencia);

public record ListProfesor(
    Guid Id,
    string Nombre,
    string Titulo,
    int Experiencia,
    List<string> Cursos
);