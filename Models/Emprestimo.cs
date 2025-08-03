using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaApi.Models;

public class Emprestimo
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string IdUsuario { get; set; } = string.Empty;
    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    public string IdLivro { get; set; } = string.Empty;
    [ForeignKey("IdLivro")]
    public Livro? Livro { get; set; }
    
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucaoPrevista { get; set; }
    public DateTime? DataDevolucaoReal { get; set; }
    public string Status { get; set; } = string.Empty;
}