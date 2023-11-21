using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosProject.DTOs;
using SophosProject.Models;
using SophosProject.PostgreSQL;

namespace SophosProject.Controllers;

[Route("Facultades")]
[ApiController]
public class FacultadController : ControllerBase
{
    private readonly UniversityDBContext _context;

    public FacultadController(UniversityDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get a list of facultades.
    /// </summary>
    /// <returns>A list of facultades.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Facultad>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<Facultad>>> GetFacultades()
    {
        return await _context.Facultades.ToListAsync();
    }

    /// <summary>
    /// Get a list of alumnos belonging to a specific facultad.
    /// </summary>
    /// <param name="id">The ID of the facultad.</param>
    /// <returns>A list of alumnos belonging to the facultad.</returns>
    [HttpGet("{id}/alumnos")]
    [ProducesResponseType(typeof(IEnumerable<Alumno>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnosFacultad(Guid id)
    {
        var facultad = await _context.Facultades.FindAsync(id);
        if (facultad == null)
        {
            return NotFound();
        }
        var alumnos = _context.Alumnos.Where(a => a.FacultadId == id).ToList();
        return alumnos ?? new List<Alumno>();
    }

    /// <summary>
    /// Create a new facultad.
    /// </summary>
    /// <param name="Facultad">The information of the new facultad to create.</param>
    /// <returns>An ActionResult containing the created facultad information.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Facultad), 201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Facultad>> CreateFacultad(CreateFacultad Facultad)
    {
        var new_Facultad = new Facultad
        {
            Nombre = Facultad.Nombre,
        };
        try
        {
            await _context.AddAsync(new_Facultad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateFacultad), new_Facultad);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update facultad information.
    /// </summary>
    /// <param name="id">The ID of the facultad to update.</param>
    /// <param name="Facultad">The updated facultad information.</param>
    /// <returns>No content if successful, or not found if the facultad doesn't exist.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Facultad>> UpdateFacultad(Guid id, UpdateFacultad Facultad)
    {
        var updatedFacultad = await _context.Facultades.FindAsync(id);
        if (updatedFacultad == null)
        {
            return NotFound();
        }

        updatedFacultad.Nombre = Facultad.Nombre ?? updatedFacultad.Nombre;

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
    /// Delete a facultad.
    /// </summary>
    /// <param name="id">The ID of the facultad to delete.</param>
    /// <returns>No content if successful, or not found if the facultad doesn't exist.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Facultad>> DeleteFacultad(Guid id)
    {
        var Facultad = await _context.Facultades.FindAsync(id);
        if (Facultad == null)
        {
            return NotFound();
        }

        _context.Facultades.Remove(Facultad);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}