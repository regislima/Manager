using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels.User
{
    public class UserViewUpdate
    {
        [Required(ErrorMessage = "O Id é obrigatório")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(3, ErrorMessage = "O Nome deve ter no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "O Nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; }

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