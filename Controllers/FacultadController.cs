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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Facultad>>> GetFacultades()
    {
        return await _context.Facultades.ToListAsync();
    }

    [HttpGet("{id}/alumnos")]
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

    [HttpPost]
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

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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