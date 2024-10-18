using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    public class TemAlunoHelper
    {
            public static bool TemAluno(string turmaCodigo, CursoContext context)
            {
                return context.alunoTurmas.Any(at => at.TurmaId == turmaCodigo);
            }
    }
}