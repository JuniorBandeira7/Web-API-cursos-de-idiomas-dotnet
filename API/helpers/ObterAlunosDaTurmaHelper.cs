using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers;
public class ObterAlunosDaTurmaHelper
{
    public static (List<Aluno>? alunos, string? mensagemErro, bool sucesso) ObterAlunosDaTurma(string codigo, CursoContext context)
    {
        // Busca os cpfs dos alunos que estão na turma
        var alunoIdsNaTurma = context.alunoTurmas
            .Where(at => at.TurmaId == codigo)
            .Select(at => at.AlunoId)
            .ToList();
        
        if (alunoIdsNaTurma.Count == 0)
        {
            return (null, "Nenhum aluno encontrado para essa turma", false);
        }

        // Busca os detalhes dos alunos na tabela Aluno
        var alunos = context.Alunos
            .Where(a => alunoIdsNaTurma.Contains(a.Cpf))
            .ToList();


        return (alunos, null, true);
    }
}
