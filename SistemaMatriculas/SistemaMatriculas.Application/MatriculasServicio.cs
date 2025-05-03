using SistemaMatriculas.Application.DTOs;
using SistemaMatriculas.Application.Interfaces;
using SistemaMatriculas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Application
{
    public class MatriculasServicio : IMatriculasServicio
    {
        private readonly IMatriculasRepositorio _matriculasRepositorio;

        public MatriculasServicio(IMatriculasRepositorio matriculasRepositorio)
        {
            _matriculasRepositorio = matriculasRepositorio;
        }

        public Matriculas Crear(Matriculas matricula)
        {
            return _matriculasRepositorio.Crear(matricula);
        }

        public Matriculas Modificar(Matriculas matricula)
        {
            return _matriculasRepositorio.Modificar(matricula);
        }
        public bool Eliminar(int idMatricula)
        {
            return _matriculasRepositorio.Eliminar(idMatricula);
        }
        public bool ValidarMatriculaExistente(int idEstudiante, int idCurso)
        {
            return _matriculasRepositorio.ValidarMatriculaExistente(idEstudiante, idCurso);
        }

        public IEnumerable<MatriculaDto> ObtenerPorId(int idMatricula)
        {
            return _matriculasRepositorio.ObtenerPorId(idMatricula);
        }

        public IEnumerable<MatriculaDto> ObtenerPorEstudiante(int idEstudiante)
        {
            return _matriculasRepositorio.ObtenerPorEstudiante(idEstudiante);
        }

        public IEnumerable<MatriculaDto> ObtenerPorCurso(int idCurso)
        {
            return _matriculasRepositorio.ObtenerPorCurso(idCurso);
        }

        public IEnumerable<MatriculaDto> ObtenerPorEstado(int idEstadoMatricula)
        {
            return _matriculasRepositorio.ObtenerPorEstado(idEstadoMatricula);
        }
    }
}
