using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Turma
    {
        [Key]
        public string Codigo { get; set; }

        public string nivel { get; set; }

        [JsonIgnore]// Usado para corrigir erro de ciclo de objeto
        public ICollection<AlunoTurma> AlunoTurmas { get; set; }
    }
}