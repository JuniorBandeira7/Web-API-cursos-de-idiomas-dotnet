using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class CursoContext : DbContext
    {
        public CursoContext(DbContextOptions<CursoContext> options): base(options) {}

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Turma> turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>()
            .HasMany(e => e.Turmas)
            .WithMany(c => c.Alunos)
            .UsingEntity<Dictionary<string, object>>(
                "AlunoTurma",
                j => j.HasOne<Turma>().WithMany().HasForeignKey("TurmaId"),
                j => j.HasOne<Aluno>().WithMany().HasForeignKey("AlunoId")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}