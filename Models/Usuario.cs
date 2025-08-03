using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models;

public class Usuario
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}