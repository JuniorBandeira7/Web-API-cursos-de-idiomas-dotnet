using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
namespace API.helpers
{
    public class LimiteAlunoHelper
    {
            public static bool LimiteAluno(string turmaCodigo, CursoContext context)
            {
                int limite = 5;
                int contador = 0;
                List<AlunoTurma> alunoTurmas = context.alunoTurmas.ToList();
                foreach (var matricula in alunoTurmas)
                {
                    if (matricula.TurmaId == turmaCodigo) contador++; 

                    if (contador == limite) return true;                   
                }
                return false;
            }
    }
}