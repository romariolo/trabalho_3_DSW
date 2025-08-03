using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaApi.Models;

public class Sessao
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string IdLivro { get; set; } = string.Empty;
    [ForeignKey("IdLivro")]
    public Livro? Livro { get; set; }

    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}