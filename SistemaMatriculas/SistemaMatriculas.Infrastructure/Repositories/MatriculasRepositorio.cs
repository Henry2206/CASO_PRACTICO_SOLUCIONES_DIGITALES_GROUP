using Microsoft.Extensions.Logging;
using SistemaMatriculas.Application.Constantes;
using SistemaMatriculas.Application.DTOs;
using SistemaMatriculas.Application.Interfaces;
using SistemaMatriculas.Domain.Entities;
using SistemaMatriculas.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Infrastructure.Repositories
{
    public class MatriculasRepositorio : IMatriculasRepositorio
    {
        private readonly SistemaMatriculasContext _context;
        private readonly ILogger<MatriculasRepositorio> _logger;
        public MatriculasRepositorio(SistemaMatriculasContext context, ILogger<MatriculasRepositorio> logger)
        {
            _context = context;
            _logger = logger;
        }

        private bool ValidarEstudianteCursoExistente(int idEstudiante, int idCurso)
        {
            var estudianteExiste = _context.Set<Estudiante>().Any(e => e.IdEstudiante == idEstudiante);
            var cursoExiste = _context.Set<Curso>().Any(c => c.IdCurso == idCurso);
            return estudianteExiste && cursoExiste;
        }
        public Matriculas Crear(Matriculas matricula)
        {
            _logger.LogInformation("Crear matrícula para estudiante {IdEstudiante} en curso {IdCurso}", matricula.IdEstudiante, matricula.IdCurso);

            if (!ValidarEstudianteCursoExistente(matricula.IdEstudiante, matricula.IdCurso))
            {
                _logger.LogWarning("Estudiante {IdEstudiante} o curso {IdCurso} no existen", matricula.IdEstudiante, matricula.IdCurso);
                throw new Exception("El estudiante o el curso no existen.");
            }

            if (ValidarMatriculaExistente(matricula.IdEstudiante, matricula.IdCurso))
            {
                _logger.LogWarning("El estudiante {IdEstudiante} ya está matriculado en el curso {IdCurso}", matricula.IdEstudiante, matricula.IdCurso);
                throw new Exception("El estudiante ya está matriculado en este curso.");
            }

            if (matricula.FechaMatricula > DateTime.Now)
            {
                _logger.LogWarning("Fecha de matrícula {FechaMatricula} es futura", matricula.FechaMatricula);
                throw new Exception("La fecha de matrícula no puede ser futura.");
            }

            _context.Set<Matriculas>().Add(matricula);
            _context.SaveChanges();

            _logger.LogInformation("Matrícula creada exitosamente con ID {IdMatricula}", matricula.IdMatricula);
            return matricula;
        }

        public Matriculas Modificar(Matriculas matricula)
        {
            _logger.LogInformation("Modificando matrícula con ID {IdMatricula}", matricula.IdMatricula);

            var matriculaExistente = _context.Set<Matriculas>().Find(matricula.IdMatricula);
            if (matriculaExistente == null)
            {
                _logger.LogWarning("Matrícula con ID {IdMatricula} no encontrada", matricula.IdMatricula);
                throw new Exception("Matrícula no encontrada.");
            }

            var estadosValidos = new[] {
                EstadoMatriculaConstantes.Activa,
                EstadoMatriculaConstantes.Cancelada,
                EstadoMatriculaConstantes.Finalizada
            };

            if (!estadosValidos.Contains(matricula.IdEstadoMatricula))
            {
                _logger.LogWarning("Estado de matrícula no válido: {Estado}", matricula.IdEstadoMatricula);
                throw new Exception("Estado de matrícula no válido.");
            }

            if (matriculaExistente.IdEstadoMatricula == EstadoMatriculaConstantes.Finalizada &&
                matricula.IdEstadoMatricula == EstadoMatriculaConstantes.Cancelada)
            {
                _logger.LogWarning("No se puede cambiar de 'Finalizada' a 'Cancelada'");
                throw new Exception("No se puede cambiar el estado a 'Cancelada' cuando la matrícula ya está 'Finalizada'.");
            }

            matriculaExistente.IdEstadoMatricula = matricula.IdEstadoMatricula;
            _context.SaveChanges();

            _logger.LogInformation("Matrícula {IdMatricula} modificada exitosamente", matricula.IdMatricula);
            return matriculaExistente;
        }

        public bool Eliminar(int idMatricula)
        {
            _logger.LogInformation("Eliminar matrícula con ID {IdMatricula}", idMatricula);

            var matricula = _context.Set<Matriculas>().Find(idMatricula);
            if (matricula == null)
            {
                _logger.LogWarning("Matrícula con ID {IdMatricula} no encontrada", idMatricula);
                throw new Exception("Matrícula no encontrada.");
            }

            if (matricula.IdEstadoMatricula != EstadoMatriculaConstantes.Cancelada)
            {
                _logger.LogWarning("No se puede eliminar matrícula con estado distinto a 'Cancelada'. ID: {IdMatricula}", idMatricula);
                return false;
            }

            _context.Set<Matriculas>().Remove(matricula);
            _context.SaveChanges();

            _logger.LogInformation("Matrícula con ID {IdMatricula} eliminada correctamente", idMatricula);
            return true;
        }
        public bool ValidarMatriculaExistente(int idEstudiante, int idCurso)
        {
            return _context.Set<Matriculas>()
                .Any(m => m.IdEstudiante == idEstudiante && m.IdCurso == idCurso);
        }
        public IEnumerable<MatriculaDto> ObtenerPorId(int idMatricula)
        {
            var query = from m in _context.Set<Matriculas>()
                        where m.IdMatricula == idMatricula
                        join estudiante in _context.Set<Estudiante>() on m.IdEstudiante equals estudiante.IdEstudiante
                        join curso in _context.Set<Curso>() on m.IdCurso equals curso.IdCurso
                        join profesor in _context.Set<Profesor>() on curso.IdProfesor equals profesor.IdProfesor
                        join estado in _context.Set<EstadoMatricula>() on m.IdEstadoMatricula equals estado.IdEstadoMatricula
                        select new MatriculaDto
                        {
                            IdMatricula = m.IdMatricula,
                            IdEstudiante = m.IdEstudiante,
                            NombreEstudiante = estudiante.Nombres + " " + estudiante.Apellidos,
                            DocumentoEstudiante = estudiante.NumeroDocumento,
                            IdCurso = m.IdCurso,
                            NombreCurso = curso.Nombre,
                            CodigoCurso = curso.Codigo,
                            NombreProfesor = profesor.Nombres + " " + profesor.Apellidos,
                            EstadoMatricula = estado.Nombre,
                            FechaMatricula = m.FechaMatricula
                        };
            return query.ToList();
        }

        public IEnumerable<MatriculaDto> ObtenerPorEstudiante(int idEstudiante)
        {
            var query = from m in _context.Set<Matriculas>()
                        where m.IdEstudiante == idEstudiante
                        join estudiante in _context.Set<Estudiante>() on m.IdEstudiante equals estudiante.IdEstudiante
                        join curso in _context.Set<Curso>() on m.IdCurso equals curso.IdCurso
                        join profesor in _context.Set<Profesor>() on curso.IdProfesor equals profesor.IdProfesor
                        join estado in _context.Set<EstadoMatricula>() on m.IdEstadoMatricula equals estado.IdEstadoMatricula
                        select new MatriculaDto
                        {
                            IdMatricula = m.IdMatricula,
                            IdEstudiante = m.IdEstudiante,
                            NombreEstudiante = estudiante.Nombres + " " + estudiante.Apellidos,
                            DocumentoEstudiante = estudiante.NumeroDocumento,
                            IdCurso = m.IdCurso,
                            NombreCurso = curso.Nombre,
                            CodigoCurso = curso.Codigo,
                            NombreProfesor = profesor.Nombres + " " + profesor.Apellidos,
                            EstadoMatricula = estado.Nombre,
                            FechaMatricula = m.FechaMatricula
                        };
            return query.ToList();
        }

        public IEnumerable<MatriculaDto> ObtenerPorCurso(int idCurso)
        {
            var query = from m in _context.Set<Matriculas>()
                        where m.IdCurso == idCurso
                        join estudiante in _context.Set<Estudiante>() on m.IdEstudiante equals estudiante.IdEstudiante
                        join curso in _context.Set<Curso>() on m.IdCurso equals curso.IdCurso
                        join profesor in _context.Set<Profesor>() on curso.IdProfesor equals profesor.IdProfesor
                        join estado in _context.Set<EstadoMatricula>() on m.IdEstadoMatricula equals estado.IdEstadoMatricula
                        select new MatriculaDto
                        {
                            IdMatricula = m.IdMatricula,
                            IdEstudiante = m.IdEstudiante,
                            NombreEstudiante = estudiante.Nombres + " " + estudiante.Apellidos,
                            DocumentoEstudiante = estudiante.NumeroDocumento,
                            IdCurso = m.IdCurso,
                            NombreCurso = curso.Nombre,
                            CodigoCurso = curso.Codigo,
                            NombreProfesor = profesor.Nombres + " " + profesor.Apellidos,
                            EstadoMatricula = estado.Nombre,
                            FechaMatricula = m.FechaMatricula
                        };
            return query.ToList();
        }

        public IEnumerable<MatriculaDto> ObtenerPorEstado(int idEstadoMatricula)
        {
            var query = from m in _context.Set<Matriculas>()
                        where m.IdEstadoMatricula == idEstadoMatricula
                        join estudiante in _context.Set<Estudiante>() on m.IdEstudiante equals estudiante.IdEstudiante
                        join curso in _context.Set<Curso>() on m.IdCurso equals curso.IdCurso
                        join profesor in _context.Set<Profesor>() on curso.IdProfesor equals profesor.IdProfesor
                        join estado in _context.Set<EstadoMatricula>() on m.IdEstadoMatricula equals estado.IdEstadoMatricula
                        select new MatriculaDto
                        {
                            IdMatricula = m.IdMatricula,
                            IdEstudiante = m.IdEstudiante,
                            NombreEstudiante = estudiante.Nombres + " " + estudiante.Apellidos,
                            DocumentoEstudiante = estudiante.NumeroDocumento,
                            IdCurso = m.IdCurso,
                            NombreCurso = curso.Nombre,
                            CodigoCurso = curso.Codigo,
                            NombreProfesor = profesor.Nombres + " " + profesor.Apellidos,
                            EstadoMatricula = estado.Nombre,
                            FechaMatricula = m.FechaMatricula
                        };
            return query.ToList();
        }

    }
}
