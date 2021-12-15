using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels
{
    public class AuthInputView
    {
        [Required(ErrorMessage = "O Email é obrigatório")]
        [MaxLength(180, ErrorMessage = "O Email deve ter no máximo 180 caracteres")]
        [EmailAddress(ErrorMessage = "O Email é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatório")]
        [MinLength(6, ErrorMessage = "A Senha deve ter no mínimo 6 caracteres")]
        [MaxLength(15, ErrorMessage = "A Senha deve ter no máximo 15 caracteres")]
        public string Password { get; set; }
    }
}