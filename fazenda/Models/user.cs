using System.ComponentModel.DataAnnotations;

namespace fazenda.Models
{
    public class User
    {
        // Identificador do usuário (geralmente é auto-incrementado)
        public int Id { get; set; }

        // Nome é obrigatório
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        // Nome Fantasia é opcional
        [StringLength(100, ErrorMessage = "O nome fantasia deve ter no máximo 100 caracteres.")]
        public string NomeFantasia { get; set; }

        // Nome Social é opcional
        [StringLength(100, ErrorMessage = "O nome social deve ter no máximo 100 caracteres.")]
        public string NomeSocial { get; set; }

        // CPF é obrigatório e deve estar no formato correto
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato XXX.XXX.XXX-XX.")]
        public string CPF { get; set; }

        // CNPJ é opcional, mas deve estar no formato correto se preenchido
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "CNPJ inválido. Use o formato XX.XXX.XXX/XXXX-XX.")]
        public string CNPJ { get; set; }

        // E-mail é obrigatório e deve ser válido
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        [StringLength(255, ErrorMessage = "O e-mail deve ter no máximo 255 caracteres.")]
        public string Email { get; set; }

        // Telefone é opcional, mas deve ser válido se preenchido
        [Phone(ErrorMessage = "Digite um telefone válido.")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        public string Telefone { get; set; }

        // Senha é obrigatória
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Password { get; set; }

        // Confirmação de senha é obrigatória e deve coincidir com a senha
        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
