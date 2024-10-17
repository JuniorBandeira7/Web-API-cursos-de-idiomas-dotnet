using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class CursoContext : DbContext
    {
        public CursoContext(DbContextOptions<CursoContext> options): base(options) {}

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Turma> turmas { get; set; }

        public DbSet<AlunoTurma> alunoTurmas{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoTurma>()
            .HasKey(ec => new { ec.AlunoId, ec.TurmaId });

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(ec => ec.Aluno)
                .WithMany(e => e.AlunoTurmas)
                .HasForeignKey(ec => ec.AlunoId);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(ec => ec.Turma)
                .WithMany(c => c.AlunoTurmas)
                .HasForeignKey(ec => ec.TurmaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}