using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosProject.DTOs;
using SophosProject.Models;
using SophosProject.PostgreSQL;

namespace SophosProject.Controllers;

[Route("alumnos")]
[ApiController]
public class AlumnoController : ControllerBase
{
    private readonly UniversityDBContext _context;

    public AlumnoController(UniversityDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
    {
        return await _context.Alumnos.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Alumno>> CreateAlumno(CreateAlumno alumno)
    {
        var new_alumno = new Alumno
        {
            Facultad = alumno.Facultad,
            Nombre = alumno.Nombre,
        };
        try
        {
            await _context.AddAsync(new_alumno);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateAlumno), new_alumno);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Alumno>> UpdateAlumno(Guid id, UpdateAlumno alumno)
    {
        var updatedAlumno = await _context.Alumnos.FindAsync(id);
        if (updatedAlumno == null)
        {
            return NotFound();
        }

        updatedAlumno.Nombre = alumno.Nombre ?? updatedAlumno.Nombre;
        updatedAlumno.Facultad = alumno.Facultad ?? updatedAlumno.Facultad;

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
    public async Task<ActionResult<Alumno>> DeleteAlumno(Guid id)
    {
        var alumno = await _context.Alumnos.FindAsync(id);
        if (alumno == null)
        {
            return NotFound();
        }

        _context.Alumnos.Remove(alumno);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}