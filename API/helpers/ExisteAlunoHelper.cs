using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    public class ExisteAlunoHelper
    {
            public static bool ExisteAluno(string alunoCpf, CursoContext context)
            {
                return context.Alunos.Any(a => a.Cpf == alunoCpf);
            }
    }
}