using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Models
{
    public class Turma
    {
        [Key]
        public string Codigo { get; set; }

        public string nivel { get; set; }

        public ICollection<AlunoTurma> AlunoTurmas { get; set; }
    }
}