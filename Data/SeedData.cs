using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Data;

public static class SeedData
{
    public static void Initialize(ApiDbContext context)
    {
        
        context.Database.EnsureCreated();

        
        if (context.Livros.Any())
        {
            return;  
        }

        var livros = new Livro[]
        {
            new Livro { Titulo = "Dom Casmurro", Autor = "Machado de Assis", Genero = "Romance", AnoPublicacao = 1899, Disponivel = true },
            new Livro { Titulo = "O Senhor dos Anéis", Autor = "J.R.R. Tolkien", Genero = "Fantasia", AnoPublicacao = 1954, Disponivel = false },
            new Livro { Titulo = "1984", Autor = "George Orwell", Genero = "Ficção Científica", AnoPublicacao = 1949, Disponivel = true }
        };
        context.Livros.AddRange(livros);

        var usuarios = new Usuario[]
        {
            new Usuario { Nome = "João Silva", Email = "joao@example.com" },
            new Usuario { Nome = "Maria Oliveira", Email = "maria@example.com" }
        };
        context.Usuarios.AddRange(usuarios);

        context.SaveChanges();
    }
}