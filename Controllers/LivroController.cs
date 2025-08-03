using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Data;
using BibliotecaApi.Models;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ApiDbContext _context;

        
        public LivrosController(ApiDbContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return await _context.Livros.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(string id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound(); 
            }

            return livro;
        }

        
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetLivro), new { id = livro.Id }, livro);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(string id, Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest(); 
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); 
        }


        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(string id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        private bool LivroExists(string id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}