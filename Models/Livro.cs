using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models;

public class Livro
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }
    public bool Disponivel { get; set; } = true;
}