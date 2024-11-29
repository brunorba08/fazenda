using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fazenda.Models
{
    public class User
    {
        [Key] // Define explicitamente como a chave primária
        public int Id { get; set; }

        [Required] // Garante que o nome é obrigatório
        [MaxLength(100)] // Limita o tamanho do nome
        public string Name { get; set; }

        [MaxLength(100)] // Nome fantasia opcional e limitado em tamanho
        public string? NomeFantasia { get; set; }

        [MaxLength(100)] // Nome social opcional e limitado em tamanho
        public string? NomeSocial { get; set; }

        [Required] // CPF é obrigatório
        [MaxLength(11)] // Limita a entrada do CPF a 11 caracteres (formato sem pontuação)
        public string CPF { get; set; }

        [MaxLength(14)] // CNPJ opcional e limitado em tamanho
        public string? CNPJ { get; set; }

        [Required] // Email é obrigatório
        [EmailAddress] // Valida o formato de email
        [MaxLength(150)] // Limita o tamanho do email
        public string Email { get; set; }

        [Phone] // Valida o formato de telefone
        [MaxLength(20)] // Telefone opcional e limitado em tamanho
        public string? Telefone { get; set; }

        [Required] // Senha é obrigatória
        [DataType(DataType.Password)] // Marca o campo como uma senha
        [MinLength(6)] // Define um tamanho mínimo para a senha
        public string Password { get; set; }

        [NotMapped] // Não salva este campo no banco de dados
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required] // Data de criação é obrigatória
        public DateTime DataCriacao { get; set; } = DateTime.Now; // Define o padrão como a data e hora atual
    }
}
