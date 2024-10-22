using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers;
public class PesquisaTurmaPorNivelHelper
{
    public static (List<Turma>? turmasEncontradas, string? mensagemErro, bool sucesso) PesquisaTurmaPorNivel(string nivel, CursoContext context)
    {
        if (string.IsNullOrWhiteSpace(nivel))
        {
            return (null, "Escreva o nivel", false);
        }

        List<Turma> turmasEncontradas = context.turmas
            .Where(a => a.nivel.Contains(nivel))
            .ToList();

        if (turmasEncontradas.Count == 0)
        {
            return (null, "Nenhuma turma encontrado com esse nivel", false);
        }

        return (turmasEncontradas, null, true);
    }
}
