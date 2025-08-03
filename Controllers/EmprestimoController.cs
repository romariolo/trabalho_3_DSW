using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Data;
using BibliotecaApi.Models;
using BibliotecaApi.Dtos;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public EmprestimosController(ApiDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
        {
            
            return await _context.Emprestimos
                                 .Include(e => e.Livro)
                                 .Include(e => e.Usuario)
                                 .ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimo(EmprestimoCreateDto emprestimoDto)
        {
            var livro = await _context.Livros.FindAsync(emprestimoDto.IdLivro);
            if (livro == null)
            {
                return BadRequest("Livro não encontrado.");
            }

            if (!livro.Disponivel)
            {
                return BadRequest("O livro não está disponível para empréstimo.");
            }

            var usuario = await _context.Usuarios.FindAsync(emprestimoDto.IdUsuario);
             if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            
            livro.Disponivel = false; 

            var novoEmprestimo = new Emprestimo
            {
                IdLivro = emprestimoDto.IdLivro,
                IdUsuario = emprestimoDto.IdUsuario,
                DataEmprestimo = DateTime.UtcNow,
                DataDevolucaoPrevista = DateTime.UtcNow.AddDays(15),
                Status = "ativo"
            };

            _context.Emprestimos.Add(novoEmprestimo);
            await _context.SaveChangesAsync();

            
            await _context.Entry(novoEmprestimo).Reference(e => e.Livro).LoadAsync();
            await _context.Entry(novoEmprestimo).Reference(e => e.Usuario).LoadAsync();
            
            return CreatedAtAction("GetEmprestimos", new { id = novoEmprestimo.Id }, novoEmprestimo);
        }

        
        [HttpPut("finalizar/{id}")]
        public async Task<IActionResult> FinalizarEmprestimo(string id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
            {
                return NotFound("Empréstimo não encontrado.");
            }

            if (emprestimo.Status == "finalizado")
            {
                return BadRequest("Este empréstimo já foi finalizado.");
            }

            var livro = await _context.Livros.FindAsync(emprestimo.IdLivro);
            if (livro != null)
            {
                livro.Disponivel = true;
            }
            
            emprestimo.Status = "finalizado";
            emprestimo.DataDevolucaoReal = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(emprestimo);
        }
    }
}