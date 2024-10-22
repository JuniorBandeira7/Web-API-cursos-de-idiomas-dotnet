using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Controllers;
using API.helpers;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("aluno/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly CursoContext _context;

        public AlunoController(CursoContext context)
        {
            _context = context;
        }
        
        // Cadastro
        [HttpPost("cadastrar_aluno")]
        public IActionResult CriarAluno(AlunoDto alunoDto,  string turmaCodigo)
        {
            var (turma, mensagem) = ObterPorCodigoHelper.ObterPorCodigo(turmaCodigo, _context);
            var alunoExistente = _context.Alunos.Any(a => a.Cpf == alunoDto.Cpf);
            var turmaExistente = _context.turmas.Any(t => t.Codigo == turmaCodigo);
            // Validações
            if (turma == null) return NotFound(mensagem);
            if (LimiteAlunoHelper.LimiteAluno(turmaCodigo, _context)) return BadRequest("Turma já possui o limite de alunos matriculados");
            if (alunoExistente) return BadRequest("Aluno já cadastrado no sistema");
            if (!turmaExistente) return BadRequest("Essa turma não existe");

            AlunoTurma matricula = new AlunoTurma{AlunoId = alunoDto.Cpf, TurmaId = turmaCodigo};

            Aluno aluno = new Aluno{ Cpf = alunoDto.Cpf, Nome = alunoDto.Nome, Email = alunoDto.Email };

            _context.Alunos.Add(aluno);
            _context.alunoTurmas.Add(matricula);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Erro ao cadastrar aluno: " + ex.Message);
            }

            return CreatedAtAction(
                nameof(ObterAluno),
                new { cpf = aluno.Cpf },
                new{aluno, turma = turma.Codigo, mensagem = "Aluno matriculado com sucesso na turma."}
            );
        }
        
        // Listagens
        [HttpGet("{cpf}")]
        public IActionResult ObterAluno(string cpf)
        {
            var aluno = _context.Alunos.Find(cpf);
            if (aluno == null) return NotFound("Aluno não encontrado.");
            
            return Ok(aluno);
        }

        [HttpGet("buscar_alunos_por_numero_de_matriculas/{qtd}")]
        public IActionResult ObterAlunosComXMatriculas(int qtd)
        {
            var (alunoTurmas, mensagem, sucesso) = FiltroAlunosQtdTurmasHelper.AlunosComXMatriculas(qtd, _context);

            if (!sucesso) return BadRequest(mensagem);

            return Ok(alunoTurmas);
        }

        [HttpGet("buscar_alunos_por_nome/{nome}")]
        public IActionResult ObterAlunosPorNome(string nome)
        {
            var (alunosEncontrados, mensagem, sucesso) = PesquisaAlunoPorNomeHelper.PesquisarAlunoPorNome(nome, _context);

            if (!sucesso) return BadRequest(mensagem);

            return Ok(alunosEncontrados);
        }

        // Edição
        [HttpPut("atualizar/{cpf}")]
        public IActionResult Atualizar(string cpf, AlunoDto alunoDto)
        {
            var alunoBanco = _context.Alunos.Find(cpf);

            if (alunoBanco == null) return NotFound();

            alunoBanco.Nome = alunoDto.Nome;
            alunoBanco.Email = alunoDto.Email;

            _context.SaveChanges();
            
            return Ok(alunoBanco);
        }


        // Exclusão
        [HttpDelete("apagar/{cpf}")]
        public IActionResult Deletar(string cpf)
        {
            var aluno = _context.Alunos.Find(cpf);

            if (aluno == null) return NotFound();

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return NoContent();
        }
    }
}