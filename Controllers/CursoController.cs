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
    public async Task<ActionResult<IEnumerable<ListCurso>>> GetCursos([FromQuery(Name = "name")] string? name, [FromQuery(Name = "cupos")] bool? cupos)
    {
        var cursos = await _context.Cursos.ToListAsync();
        //Filters
        if (name != null)
        {
            cursos = cursos.Where(a => a.Nombre.Contains(name)).ToList();
        }

        if (cupos != null)
        {
            if ((bool)cupos)
            {
                cursos = cursos.Where(c => c.Creditos > 0).ToList();
            }
            else
            {
                cursos = cursos.Where(c => c.Creditos == 0).ToList();
            }
        }

        var listCursos = cursos.Select(c => new ListCurso(
            c.Id,
            c.Nombre,
            c.PreRequisito?.Nombre,
            c.Creditos,
            c.Cupos - _context.CursoAlumnos.Where(ca => ca.CursoId == c.Id && ca.Estado == Estado.en_curso).Count()
        ));

        return CreatedAtAction(nameof(GetCursos), listCursos);
    }

    [HttpPost]
    public async Task<ActionResult<Curso>> CreateCurso(CreateCurso Curso)
    {
        var new_Curso = new Curso
        {
            Nombre = Curso.Nombre,
            Cupos = Curso.Cupos ?? 0,
            Descripcion = Curso.Descripcion,
            PreRequisitoId = Curso.PrerequisitoId,
            ProfesorId = Curso.ProfesorId,
            Creditos = Curso.Creditos ?? 1
        };

        await _context.AddAsync(new_Curso);

        if (Curso.PrerequisitoId != null)
        {
            var prerequisito = await _context.Cursos.FindAsync(Curso.PrerequisitoId);
            if (prerequisito == null)
            {
                return NotFound("Prerequisito not found");
            }
            prerequisito.CursosSiguientes.Add(new_Curso);
        }
        var profesor = await _context.Profesores.FindAsync(Curso.ProfesorId);
        if (profesor == null)
        {
            return NotFound("Profesor not found");
        }
        profesor.Cursos.Add(new_Curso);
        try
        {
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
        updatedCurso.Creditos = Curso.Creditos ?? updatedCurso.Creditos;
        updatedCurso.Descripcion = Curso.Descripcion ?? updatedCurso.Descripcion;
        updatedCurso.PreRequisitoId = Curso.PrerequisitoId ?? updatedCurso.PreRequisitoId;
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