using System;

namespace fazenda.Models
{
    public class Favorite
    {
        public int Id { get; set; } // Chave prim�ria
        public int UserId { get; set; } // Id do usu�rio que favoritou
        public string ProductName { get; set; } // Nome do produto favoritado
        public DateTime DateAdded { get; set; } // Data em que o produto foi adicionado
    }
}
