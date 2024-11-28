using System.ComponentModel.DataAnnotations;

using System; // Necessário para DateTime e outros tipos do sistema

namespace fazenda.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeSocial { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DataCriacao { get; set; } // Agora DateTime será reconhecido
    }
}


