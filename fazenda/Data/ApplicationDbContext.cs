using Microsoft.EntityFrameworkCore;
using fazenda.Models;

namespace fazenda.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Definindo os DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        // Construtor para inje��o de depend�ncia do DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Configura��o do modelo (caso precise de ajustes adicionais)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // O EF Core j� ir� considerar 'Id' como chave prim�ria se seguir a conven��o padr�o.
            // Portanto, n�o � necess�rio configurar manualmente, mas se for necess�rio, voc� pode configurar como abaixo.
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Isso � redundante se 'Id' for a chave prim�ria por conven��o
        }
    }
}
