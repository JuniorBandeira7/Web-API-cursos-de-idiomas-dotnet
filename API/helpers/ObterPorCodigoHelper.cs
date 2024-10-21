using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    public class ObterPorCodigoHelper
    {
            public static (Turma? turma, string? mensagen) ObterPorCodigo(string turmaCodigo, CursoContext context)
            {
                if (context.turmas.Find(turmaCodigo) == null) return (null, "Usuário não encontrado");

                Turma turma = context.turmas.Find(turmaCodigo);
                return (turma, null);
            }
    }
}