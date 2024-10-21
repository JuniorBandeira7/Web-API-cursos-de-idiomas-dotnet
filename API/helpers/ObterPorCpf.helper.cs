using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    public class ObterPorCpfHelper
    {
            public static (Aluno? aluno, string? mensagen) ObterPorCpf(string alunoCpf, CursoContext context)
            {
                if (context.Alunos.Find(alunoCpf) == null) return (null, "Usuário não encontrado");

                Aluno aluno = context.Alunos.Find(alunoCpf);
                return (aluno, null);
            }
    }
}