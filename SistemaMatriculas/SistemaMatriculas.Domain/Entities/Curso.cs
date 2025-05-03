using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Domain.Entities
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        [Required]
        [MaxLength(10)]
        public string Codigo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public int IdProfesor { get; set; }
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
