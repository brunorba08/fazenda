using Microsoft.EntityFrameworkCore;
using fazenda.Models;  // Adapte conforme o modelo de dados (ex. User)

namespace fazenda.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		// Defina um DbSet para cada modelo de dados que você deseja mapear para o banco
		public DbSet<User> Users { get; set; }  // Exemplo de tabela Users
	}
}
