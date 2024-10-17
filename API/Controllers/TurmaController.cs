using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("turma/[controller]")]
    public class TurmaController : ControllerBase
    {
        private readonly CursoContext _context;

        public TurmaController(CursoContext context)
        {
            _context = context;
        }
    }
}