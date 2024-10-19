using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers;
public class PesquisaAlunoPorNomeHelper
{
    public static (List<Aluno>? alunosEncontrados, string? mensagemErro, bool sucesso) PesquisarAlunoPorNome(string nome, CursoContext context)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return (null, "Nome não pode ser vazio", false);
        }

        List<Aluno> alunosEncontrados = context.Alunos
            .Where(a => a.Nome.Contains(nome))
            .ToList();

        if (alunosEncontrados.Count == 0)
        {
            return (null, "Nenhum aluno encontrado com esse nome", false);
        }

        return (alunosEncontrados, null, true);
    }
}
