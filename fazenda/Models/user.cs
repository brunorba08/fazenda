using System.ComponentModel.DataAnnotations;  // Necessário para usar as anotações de validação

namespace fazenda.Models
{
    public class User
    {
        // Atributo para validar que o campo é obrigatório e que o formato do e-mail é válido
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get; set; }

        // Atributo para validar que o campo é obrigatório e deve ser tratado como senha
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]  // Este tipo indica que o campo é para senha
        public string Password { get; set; }
    }
}
