using System.ComponentModel.DataAnnotations;

namespace SophosProject.DTOs;

public record CreateFacultad([Required] string Nombre);

public record UpdateFacultad([Required] string? Nombre);