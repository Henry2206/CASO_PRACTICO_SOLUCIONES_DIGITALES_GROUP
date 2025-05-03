using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Domain.Entities
{
    public class Profesor
    {
        [Key]
        public int IdProfesor { get; set; }
        [Required]
        [MaxLength(20)]
        public string NumeroDocumento { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Especialidad { get; set; }
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
