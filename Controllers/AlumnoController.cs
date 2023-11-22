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

    /// <summary>
    /// Get a list of alumnos with optional filtering by name and facultad.
    /// </summary>
    /// <param name="name">The name of the alumno to filter by.</param>
    /// <param name="facultad">The facultad to filter by.</param>
    /// <returns>A list of alumnos.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ListAlumno>), 200)]
    [ProducesResponseType(500)]
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
            alumno.Id,
            alumno.Nombre,
            _context.Facultades.Find(alumno.FacultadId)?.Nombre,
            _context.CursoAlumnos.Where(ca => ca.AlumnoId == alumno.Id).Sum(ca => ca.Curso.Creditos)
        ));

        return CreatedAtAction(nameof(GetAlumnos), listAlumnos);
    }

    /// <summary>
    /// Get information about a specific student by ID.
    /// </summary>
    /// <param name="id">The ID of the student.</param>
    /// <returns>An ActionResult containing the student information.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetAlumno), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<GetAlumno>> GetAlumno(Guid id)
    {
        var alumno = await _context.Alumnos.FindAsync(id);

        if (alumno == null)
        {
            return NotFound();
        }

        var matriculados = _context.CursoAlumnos
            .Where(ca => ca.AlumnoId == id && ca.Estado == Estado.en_curso)
            .Join(
                _context.Cursos,
                ca => ca.CursoId,
                c => c.Id,
                (ca, c) => c
            )
            .ToList();

        var cursados = _context.CursoAlumnos
            .Where(ca => ca.AlumnoId == id && ca.Estado == Estado.cursado)
            .Join(
                _context.Cursos,
                ca => ca.CursoId,
                c => c.Id,
                (ca, c) => c
            )
            .ToList();

        var selected = new GetAlumno(
            alumno.Id,
            alumno.Nombre,
            matriculados.Sum(m => m.Creditos),
            matriculados,
            cursados
        );

        return CreatedAtAction(nameof(GetAlumno), selected);
    }

    /// <summary>
    /// Create a new student.
    /// </summary>
    /// <param name="alumno">The information of the new student to create.</param>
    /// <returns>An ActionResult containing the created student information.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Alumno), 201)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(500)]
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

    /// <summary>
    /// Matricula a student for a course.
    /// </summary>
    /// <param name="alumno_id">The ID of the student.</param>
    /// <param name="curso_id">The ID of the course.</param>
    /// <returns>A string indicating the result of the matriculation process.</returns>
    [HttpPost("{alumno_id}/matricula/{curso_id}")]
    [ProducesResponseType(typeof(string), 201)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<string>> MatriculaAlumno(Guid alumno_id, Guid curso_id)
    {
        var alumno = _context.Alumnos.Find(alumno_id);
        var curso = _context.Cursos.Find(curso_id);

        if (alumno == null)
        {
            return NotFound("Alumno no encontrado");
        }
        if (curso == null)
        {
            return NotFound("Curso no encontrado");
        }
        //Look for previous relationships
        var previous = _context.CursoAlumnos.Where(ca => ca.AlumnoId == alumno_id && ca.CursoId == curso_id).ToList();
        if (previous.Count == 0)
        {
            //Check if alumno has passed the prerequisite
            var prerequisite = _context.CursoAlumnos.Where(ca => ca.AlumnoId == alumno_id && ca.CursoId == curso.PreRequisitoId).ToList();
            if (prerequisite.Count > 0)
            {
                if (prerequisite[0]?.Estado == Estado.en_curso)
                {
                    return BadRequest("Prerrequisito no aprobado");
                }

            }
            var CuposDisponibles = curso.Cupos - _context.CursoAlumnos.Where(ca => ca.CursoId == curso.Id && ca.Estado == Estado.en_curso).Count();
            if (CuposDisponibles > 0)
            {
                try
                {
                    var matricula = new CursoAlumno
                    {
                        AlumnoId = alumno_id,
                        Alumno = alumno,
                        CursoId = curso_id,
                        Curso = curso,
                        Estado = Estado.en_curso,
                    };

                    alumno.CursoAlumnos.Add(matricula);
                    curso.CursoAlumnos.Add(matricula);

                    await _context.AddAsync(matricula);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(MatriculaAlumno), "Alumno matriculado exitosamente");
                }
                catch (System.Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest("Curso sin cupos disponibles");
            }
        }
        else
        {
            return BadRequest("Alumno previamente matriculado");
        }
    }

    /// <summary>
    /// Update student information.
    /// </summary>
    /// <param name="id">The ID of the student to update.</param>
    /// <param name="alumno">The updated student information.</param>
    /// <returns>No content if successful, or not found if the student doesn't exist.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
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

    /// <summary>
    /// Delete a student.
    /// </summary>
    /// <param name="id">The ID of the student to delete.</param>
    /// <returns>No content if successful, or not found if the student doesn't exist.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
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