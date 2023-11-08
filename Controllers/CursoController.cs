using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophosProject.DTOs;
using SophosProject.Models;
using SophosProject.PostgreSQL;

namespace SophosProject.Controllers;

[Route("Cursos")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly UniversityDBContext _context;

    public CursoController(UniversityDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
    {
        return await _context.Cursos.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> CreateCurso(CreateCurso Curso)
    {
        var new_Curso = new Curso
        {
            Nombre = Curso.Nombre,
            Cupos = Curso.Cupos ?? 0,
            Descripcion = Curso.Descripcion,
            PrerequisitoId = Curso.PrerequisitoId,
            ProfesorId = Curso.ProfesorId
        };
        try
        {
            await _context.AddAsync(new_Curso);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateCurso), new_Curso);
        }
        catch (System.Exception)
        {

            return StatusCode(500);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Curso>> UpdateCurso(Guid id, UpdateCurso Curso)
    {
        var updatedCurso = await _context.Cursos.FindAsync(id);
        if (updatedCurso == null)
        {
            return NotFound();
        }

        updatedCurso.Nombre = Curso.Nombre ?? updatedCurso.Nombre;
        updatedCurso.Cupos = Curso.Cupos ?? updatedCurso.Cupos;
        updatedCurso.Descripcion = Curso.Descripcion ?? updatedCurso.Descripcion;
        updatedCurso.PrerequisitoId = Curso.PrerequisitoId ?? updatedCurso.PrerequisitoId;
        updatedCurso.ProfesorId = Curso.ProfesorId ?? updatedCurso.ProfesorId;

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
    public async Task<ActionResult<Curso>> DeleteCurso(Guid id)
    {
        var Curso = await _context.Cursos.FindAsync(id);
        if (Curso == null)
        {
            return NotFound();
        }

        _context.Cursos.Remove(Curso);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}