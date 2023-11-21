using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosProject.DTOs;
using SophosProject.Models;
using SophosProject.PostgreSQL;

namespace SophosProject.Controllers;

[Route("Profesores")]
[ApiController]
public class ProfesorController : ControllerBase
{
    private readonly UniversityDBContext _context;

    public ProfesorController(UniversityDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a list of profesores with optional filtering by name.
    /// </summary>
    /// <param name="name">The name of the profesor to filter by.</param>
    /// <returns>A list of profesores.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListProfesor>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ListProfesor>>> GetProfesores([FromQuery(Name = "name")] string? name)
    {
        var profesores = await _context.Profesores.ToListAsync();
        //Filters
        if (name != null)
        {
            profesores = profesores.Where(a => a.Nombre.Contains(name)).ToList();
        }

        var listProfesores = profesores.Select(p => new ListProfesor(
            p.Id,
            p.Nombre,
            p.Titulo,
            p.Experiencia,
            _context.Cursos.Select(curso => curso.Nombre).ToList()
        ));
        return CreatedAtAction(nameof(GetProfesores), listProfesores);
    }

    /// <summary>
    /// Create a new profesor.
    /// </summary>
    /// <param name="Profesor">The information of the new profesor to create.</param>
    /// <returns>An ActionResult containing the created profesor information.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Profesor), 201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Profesor>> CreateProfesor(CreateProfesor Profesor)
    {
        var new_Profesor = new Profesor
        {
            Nombre = Profesor.Nombre,
            Experiencia = Profesor.Experiencia,
            Titulo = Profesor.Titulo
        };
        try
        {
            await _context.AddAsync(new_Profesor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateProfesor), new_Profesor);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update profesor information.
    /// </summary>
    /// <param name="id">The ID of the profesor to update.</param>
    /// <param name="Profesor">The updated profesor information.</param>
    /// <returns>No content if successful, or not found if the profesor doesn't exist.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Profesor>> UpdateProfesor(Guid id, UpdateProfesor Profesor)
    {
        var updatedProfesor = await _context.Profesores.FindAsync(id);
        if (updatedProfesor == null)
        {
            return NotFound();
        }

        updatedProfesor.Nombre = Profesor.Nombre ?? updatedProfesor.Nombre;
        updatedProfesor.Experiencia = Profesor.Experiencia ?? updatedProfesor.Experiencia;
        updatedProfesor.Titulo = Profesor.Titulo ?? updatedProfesor.Titulo;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a profesor.
    /// </summary>
    /// <param name="id">The ID of the profesor to delete.</param>
    /// <returns>No content if successful, or not found if the profesor doesn't exist.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Profesor>> DeleteProfesor(Guid id)
    {
        var Profesor = await _context.Profesores.FindAsync(id);
        if (Profesor == null)
        {
            return NotFound();
        }

        _context.Profesores.Remove(Profesor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}