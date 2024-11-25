using System.ComponentModel.DataAnnotations;

namespace fazenda.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A senha e a confirma��o de senha n�o coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
