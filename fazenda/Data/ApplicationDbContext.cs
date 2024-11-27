using Microsoft.EntityFrameworkCore;
using fazenda.Models;  // Certifique-se de que a classe User est� neste namespace

namespace fazenda.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor que recebe op��es de contexto de banco de dados
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para o modelo User
        public DbSet<User> Users { get; set; }  // Exemplo de tabela Users
    }
}
