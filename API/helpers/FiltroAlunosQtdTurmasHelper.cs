using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    // Filtra alunos com quantidade especifica de matriculas em turmas.
    public class FiltroAlunosQtdTurmasHelper
    {
            public static (List<AlunoTurma>? alunosComQtdXMatriculas, string? mensagemErro, bool sucesso) AlunosComXMatriculas(int numeroAlunos, CursoContext context)
            {
                if (numeroAlunos <= 0) return (null, "Digite um numero válido", false);

                List<AlunoTurma> alunoTurmas = context.alunoTurmas.ToList();
                List<AlunoTurma> alunosComQtdXMatriculas = new List<AlunoTurma>();

                foreach (var matricula in alunoTurmas)
                {
                    int contador = alunoTurmas.Count(m => m.AlunoId == matricula.AlunoId);

                    if (contador == numeroAlunos && !alunosComQtdXMatriculas.Any(t => t.AlunoId == matricula.AlunoId)) alunosComQtdXMatriculas.Add(matricula);
                }
                int numeroDeMatriculas = alunosComQtdXMatriculas.Count;
                if (numeroDeMatriculas < 1) return (null, "Não existe alunos com essa quantidade de matriculas", false);
                return (alunosComQtdXMatriculas, null, true);
            }
    }
}