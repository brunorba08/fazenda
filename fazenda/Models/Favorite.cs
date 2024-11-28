using System;

namespace fazenda.Models
{
    public class Favorite
    {
        public int Id { get; set; } // Chave primária
        public int UserId { get; set; } // Id do usuário que favoritou
        public string ProductName { get; set; } // Nome do produto favoritado
        public DateTime DateAdded { get; set; } // Data em que o produto foi adicionado
    }
}
