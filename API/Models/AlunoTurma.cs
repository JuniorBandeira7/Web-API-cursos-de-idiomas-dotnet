using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Models
{
    public class AlunoTurma
    {
    public string AlunoId { get; set; }
    public Aluno Aluno { get; set; }

    public string TurmaId { get; set; }
    public Turma Turma { get; set; }
    }
}