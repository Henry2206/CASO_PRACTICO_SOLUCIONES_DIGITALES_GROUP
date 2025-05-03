using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Domain.Entities
{
    public class Matriculas
    {
        [Key]
        public int IdMatricula { get; set; }
        [Required]
        public int IdEstudiante { get; set; }
        [Required]
        public int IdCurso { get; set; }
        [Required]
        public int IdEstadoMatricula { get; set; }
        [Required]
        public DateTime FechaMatricula { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [Required]
        [MaxLength(50)]
        public string UsuarioCreacion { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}
