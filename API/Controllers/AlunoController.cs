using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;

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
    }
}