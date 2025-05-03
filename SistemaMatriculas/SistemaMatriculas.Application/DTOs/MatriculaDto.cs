using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Application.DTOs
{
    public class MatriculaDto
    {
        public int IdMatricula { get; set; }
        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public string DocumentoEstudiante { get; set; }
        public int IdCurso { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public string NombreProfesor { get; set; }
        public string EstadoMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }
    }
}
