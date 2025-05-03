using Microsoft.EntityFrameworkCore;
using SistemaMatriculas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMatriculas.Infrastructure.Data
{
    public class SistemaMatriculasContext : DbContext
    {
        public SistemaMatriculasContext(DbContextOptions<SistemaMatriculasContext> options) : base(options) { }
        public DbSet<Matriculas> Matricula { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<EstadoMatricula> EstadoMatricula { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
    }
}
