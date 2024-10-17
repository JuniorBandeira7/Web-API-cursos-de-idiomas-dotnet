using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    public class CadastroUnico
    {
            public static bool AlunoJaMatriculado(string alunoCpf, string turmaCodigo, CursoContext context)
            {
                return context.alunoTurmas.Any(at => at.AlunoId == alunoCpf && at.TurmaId == turmaCodigo);
            }
    }
}