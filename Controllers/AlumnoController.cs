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
    public async Task<ActionResult<IEnumerable<ListAlumno>>> GetAlumnos([FromQuery(Name = "name")] string? name, [FromQuery(Name = "facultad")] string? facultad)
    {
        var alumnos = await _context.Alumnos.ToListAsync();

        //Filters
        if (name != null)
        {
            alumnos = alumnos.Where(a => a.Nombre.Contains(name)).ToList();
        }

        if (facultad != null)
        {
            alumnos = alumnos.Where(a => _context.Facultades.Find(a.FacultadId)?.Nombre == facultad).ToList();
        }

        var listAlumnos = alumnos.Select(alumno => new ListAlumno(
            alumno.Nombre,
            _context.Facultades.Find(alumno.FacultadId)?.Nombre,
            _context.CursoAlumnos.Where(ca => ca.AlumnoId == alumno.Id).Sum(ca => ca.Curso.Creditos)
        ));

        return CreatedAtAction(nameof(GetAlumnos), listAlumnos);
    }

    [HttpPost]
    public async Task<ActionResult<Alumno>> CreateAlumno(CreateAlumno alumno)
    {
        try
        {
            // Create a new Alumno
            var newAlumno = new Alumno
            {
                Nombre = alumno.Nombre,
                FacultadId = alumno.FacultadId
            };


            // Find the corresponding Facultad and add the new Alumno to its collection
            var facultad = await _context.Facultades.FindAsync(alumno.FacultadId);
            if (facultad == null)
            {
                // Handle the case where the associated Facultad doesn't exist
                return NotFound("Facultad not found");
            }

            // Add the new Alumno to the context
            _context.Add(newAlumno);
            facultad.Alumnos.Add(newAlumno);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the created Alumno
            return CreatedAtAction(nameof(CreateAlumno), newAlumno);
        }
        catch (System.Exception)
        {
            // Handle exceptions appropriately
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
        updatedAlumno.FacultadId = alumno.FacultadId ?? updatedAlumno.FacultadId;

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