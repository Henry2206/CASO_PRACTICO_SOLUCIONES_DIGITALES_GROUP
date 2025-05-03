using Microsoft.AspNetCore.Mvc;
using SistemaMatriculas.Application.DTOs;
using SistemaMatriculas.Application;
using SistemaMatriculas.Application.Interfaces;
using SistemaMatriculas.Domain.Entities;
using System;

namespace SistemaMatriculas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly IMatriculasServicio _matriculasServicio;

        public MatriculasController(IMatriculasServicio matriculasServicio)
        {
            _matriculasServicio = matriculasServicio;
        }

        [HttpPost]
        public IActionResult CrearMatricula([FromBody] Matriculas matricula)
        {
            try
            {
                var resultado = _matriculasServicio.Crear(matricula);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult ModificarEstadoMatricula([FromBody] Matriculas matricula)
        {
            try
            {
                var resultado = _matriculasServicio.Modificar(matricula);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarMatricula(int id)
        {
            try
            {
                var eliminado = _matriculasServicio.Eliminar(id);
                if (eliminado)
                {
                    return NoContent();
                }
                return NotFound("La matrícula no se puede eliminar, el estado no es 'Cancelada'.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ObtenerPorMatricula/{id}")]
        public ActionResult<IEnumerable<MatriculaDto>> ObtenerPorId(int id)
        {
            return Ok(_matriculasServicio.ObtenerPorId(id));
        }

        [HttpGet("ObtenerPorEstudiante/{idEstudiante}")]
        public ActionResult<IEnumerable<MatriculaDto>> ObtenerPorEstudiante(int idEstudiante)
        {
            return Ok(_matriculasServicio.ObtenerPorEstudiante(idEstudiante));
        }

        [HttpGet("ObtenerPorCurso/{idCurso}")]
        public ActionResult<IEnumerable<MatriculaDto>> ObtenerPorCurso(int idCurso)
        {
            return Ok(_matriculasServicio.ObtenerPorCurso(idCurso));
        }

        [HttpGet("ObtenerPorEstado/{idEstado}")]
        public ActionResult<IEnumerable<MatriculaDto>> ObtenerPorEstado(int idEstado)
        {
            return Ok(_matriculasServicio.ObtenerPorEstado(idEstado));
        }
    }
}
