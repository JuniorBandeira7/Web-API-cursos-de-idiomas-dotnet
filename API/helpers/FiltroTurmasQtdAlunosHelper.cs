using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    // Filtra turmas com quantidade especifica de alunos.
    public class FiltroTurmasQtdAlunosHelper
    {
            public static (List<Turma>? turmaComQtdXAlunos, string? mensagemErro, bool sucesso) TurmaComXAlunos(int numeroAlunos, CursoContext context)
            {
                int limite = 5;

                if (numeroAlunos >= limite) return (null, "Numero de alunos maior que limite", false);
                if (numeroAlunos <= 0) return (null, "Digite um numero válido", false);

                List<AlunoTurma> alunoTurmas = context.alunoTurmas.ToList();
                List<Turma> turmaComQtdXAlunos = new List<Turma>();

                foreach (var matricula in alunoTurmas)
                {
                    int contador = alunoTurmas.Count(m => m.TurmaId == matricula.TurmaId);

                    if (contador == numeroAlunos && !turmaComQtdXAlunos.Any(t => t.Codigo == matricula.TurmaId)) turmaComQtdXAlunos.Add(context.turmas.Find(matricula.TurmaId));
                }
                int numeroDeMatriculas = turmaComQtdXAlunos.Count;
                if (numeroDeMatriculas < 1) return (null, "Não existe turma com essa quantidade de alunos", false);
                return (turmaComQtdXAlunos, null, true);
            }
    }
}
