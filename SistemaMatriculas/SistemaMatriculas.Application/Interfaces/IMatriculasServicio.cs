using SistemaMatriculas.Application.DTOs;
using SistemaMatriculas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Application.Interfaces
{
    public interface IMatriculasServicio
    {
        Matriculas Crear(Matriculas matricula);
        Matriculas Modificar(Matriculas matricula);
        bool Eliminar(int idMatricula);
        bool ValidarMatriculaExistente(int idEstudiante, int idCurso);
        IEnumerable<MatriculaDto> ObtenerPorId(int idMatricula);
        IEnumerable<MatriculaDto> ObtenerPorEstudiante(int idEstudiante);
        IEnumerable<MatriculaDto> ObtenerPorCurso(int idCurso);
        IEnumerable<MatriculaDto> ObtenerPorEstado(int idEstadoMatricula);
    }
}
