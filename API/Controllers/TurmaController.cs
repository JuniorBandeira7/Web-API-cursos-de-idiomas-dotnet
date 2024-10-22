using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.helpers;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly CursoContext _context;

        public TurmaController(CursoContext context)
        {
            _context = context;
        }
        // Listagens
        [HttpGet("{codigo}")]
        public IActionResult ObterPorCodigo(string codigo)
        {
            var turma = _context.turmas.Find(codigo);

            if (turma == null) return NotFound();

            var (alunos, mensagem, sucesso) = ObterAlunosDaTurmaHelper.ObterAlunosDaTurma(codigo, _context);

            if (!sucesso) return NotFound(mensagem);

            return Ok(new { turma, alunos }); 
        }

        [HttpGet("buscar_turma_por_numero_de_matriculas/{qtd}")]
        public IActionResult ObterTurmasComXMatriculas(int qtd)
        {
            var (turmaComQtdXAlunos, mensagem, sucesso) = FiltroTurmasQtdAlunosHelper.TurmaComXAlunos(qtd, _context);

            if (!sucesso) return NotFound(mensagem);

            return Ok(turmaComQtdXAlunos);
        }

        [HttpGet("buscar_turma_por_nivel/{nivel}")]
        public IActionResult ObterTurmasPorNivel(string nivel)
        {
            var (turmasEncontradas, mensagem, sucesso) = PesquisaTurmaPorNivelHelper.PesquisaTurmaPorNivel(nivel, _context);

            if (!sucesso) return NotFound(mensagem);

            return Ok(turmasEncontradas);
        }

        // Cadastro
        [HttpPost("cadastrar_turma")]
        public IActionResult CriarTurma(TurmaDto turmaDto)
        {
            Turma turma = new Turma{ Codigo = turmaDto.Codigo, nivel = turmaDto.nivel};

            _context.turmas.Add(turma);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Erro ao cadastrar turma: " + ex.Message);
            }
            return CreatedAtAction(
                nameof(ObterPorCodigo),
                new { codigo = turma.Codigo },
                new{turma, mensagem = "Turma cadastrada!."}
            );
        }

        [HttpPost("cadastrar_aluno_em_turma/{codigo}")]
        public IActionResult CadastrarAlunoEmTurma(string codigo, string cpf)
        {
            var (aluno, mensagem) = ObterPorCpfHelper.ObterPorCpf(cpf, _context);
            var turmaExistente = _context.turmas.Any(t => t.Codigo == codigo);
            var alunoExistente = _context.Alunos.Any(a => a.Cpf == cpf);
            // Validações
            if (aluno == null) return NotFound(mensagem);
            if (LimiteAlunoHelper.LimiteAluno(codigo, _context)) return BadRequest("Turma já possui o limite de alunos matriculados");
            if (!turmaExistente) return NotFound("Essa turma não existe");
            if (!alunoExistente) return NotFound("Aluno não encontrado no sistema");
            if (MatriculaUnicaHelper.AlunoJaMatriculado(cpf, codigo, _context)) return BadRequest("Aluno já matriculado na turma");

            AlunoTurma matricula = new AlunoTurma{AlunoId = cpf, TurmaId = codigo};

            _context.alunoTurmas.Add(matricula);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Erro ao matricular aluno: " + ex.Message);
            }

            return CreatedAtAction(
                nameof(ObterPorCodigo),
                new { codigo },
                new{matricula, mensagem = "Aluno matriculado com sucesso na turma."}
            );
        }

        // Exclusão
        [HttpDelete("apagar/{codigo}")]
        public IActionResult Deletar(string codigo)
        {
            var turma = _context.turmas.Find(codigo);
            if (turma == null) return NotFound();
            // Verifica se existe algum aluno na turma
            bool TemAluno = _context.alunoTurmas.Any(at => at.TurmaId == codigo);
            if (TemAluno) return BadRequest("Não é possível excluir turmas com alunos!");

            _context.turmas.Remove(turma);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Erro ao apagar turma: " + ex.Message);
            }

            return NoContent();
        }
    }
}