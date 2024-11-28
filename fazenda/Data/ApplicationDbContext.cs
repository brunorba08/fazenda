using Microsoft.EntityFrameworkCore;
using fazenda.Models;

namespace fazenda.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Definindo os DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        // Construtor para injeção de dependência do DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Configuração do modelo (caso precise de ajustes adicionais)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // O EF Core já irá considerar 'Id' como chave primária se seguir a convenção padrão.
            // Portanto, não é necessário configurar manualmente, mas se for necessário, você pode configurar como abaixo.
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Isso é redundante se 'Id' for a chave primária por convenção
        }
    }
}
