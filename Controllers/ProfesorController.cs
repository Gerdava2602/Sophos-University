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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores()
    {
        return await _context.Profesores.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Profesor>> CreateProfesor(CreateProfesor Profesor)
    {
        var new_Profesor = new Profesor
        {
            Nombre = Profesor.Nombre,
            Experiencia = Profesor.Experiencia,
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

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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