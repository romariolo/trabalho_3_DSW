using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Data;
using BibliotecaApi.Models;
using BibliotecaApi.Dtos;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessoesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public SessoesController(ApiDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sessao>>> GetSessoes()
        {
            return await _context.Sessoes
                                 .Include(s => s.Livro) 
                                 .ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<Sessao>> PostSessao(SessaoCreateDto sessaoDto)
        {
            var livro = await _context.Livros.FindAsync(sessaoDto.IdLivro);
            if (livro == null)
            {
                return BadRequest("Livro não encontrado.");
            }

            var novaSessao = new Sessao
            {
                IdLivro = sessaoDto.IdLivro,
                DataInicio = sessaoDto.DataInicio,
                DataFim = sessaoDto.DataFim
            };

            _context.Sessoes.Add(novaSessao);
            await _context.SaveChangesAsync();
            
            await _context.Entry(novaSessao).Reference(s => s.Livro).LoadAsync();

            return CreatedAtAction("GetSessoes", new { id = novaSessao.Id }, novaSessao);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessao(string id)
        {
            var sessao = await _context.Sessoes.FindAsync(id);
            if (sessao == null)
            {
                return NotFound();
            }

            _context.Sessoes.Remove(sessao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}