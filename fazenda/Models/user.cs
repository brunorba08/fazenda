using System.ComponentModel.DataAnnotations;

namespace fazenda.Models
{
    public class User
    {
        // Atributo para validar que o campo Name é obrigatório
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        // Atributo para validar que o campo Email é obrigatório e que o formato é válido
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get; set; }

        // Atributo para validar que o campo Password é obrigatório e deve ser tratado como senha
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Atributo para confirmar a senha, comparando com o campo Password
        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
