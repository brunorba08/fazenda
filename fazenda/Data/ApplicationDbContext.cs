using fazenda.Models;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir uma chave prim�ria para o User, assumindo que 'Id' � a chave prim�ria
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Substitua 'Id' pela sua propriedade que representa a chave prim�ria, caso necess�rio

            // Caso o 'User' n�o tenha uma chave prim�ria, voc� pode manter o HasNoKey(), mas geralmente para persist�ncia � necess�rio ter uma chave
            // modelBuilder.Entity<User>().HasNoKey();
        }
    }
}
