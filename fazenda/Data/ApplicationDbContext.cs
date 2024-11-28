using fazenda.Models;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir uma chave primária para o User, assumindo que 'Id' é a chave primária
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Substitua 'Id' pela sua propriedade que representa a chave primária, caso necessário

            // Caso o 'User' não tenha uma chave primária, você pode manter o HasNoKey(), mas geralmente para persistência é necessário ter uma chave
            // modelBuilder.Entity<User>().HasNoKey();
        }
    }
}
