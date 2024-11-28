using System;
using System.ComponentModel.DataAnnotations;

namespace fazenda.Models
{
    public class User
    {
        public int Id { get; set; } // Mapeia a coluna ID

        public string Name { get; set; } // Mapeia a coluna Name

        public string? NomeFantasia { get; set; }
        public string? NomeSocial { get; set; }

        public string CPF { get; set; } // Mapeia a coluna CPF

        public string? CNPJ { get; set; }

        public string Email { get; set; } // Mapeia a coluna Email
        public string? Telefone { get; set; }

        public string Password { get; set; } // Mapeia a coluna Password

        public string ConfirmPassword { get; set; } // Mapeia a coluna ConfirmPassword

        public DateTime DataCriacao { get; set; } // Adicione para mapear DataCriacao
    }
}
