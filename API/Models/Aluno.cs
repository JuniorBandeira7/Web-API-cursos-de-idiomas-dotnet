using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace API.Models
{
    public class Aluno
    {
        [Key]
        public string Cpf { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public ICollection<Turma> Turmas { get; set; }
    }
}