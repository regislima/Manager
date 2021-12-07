using FluentValidation;
using Manager.Domain.entities;

namespace Manager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user)
                .NotEmpty()
                .NotNull()
                .WithMessage("User não pode ser nulo ou vazio");
            
            RuleFor(user => user.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome não pode ser nulo ou vazio")

                .MinimumLength(3)
                .WithMessage("Nome deve ter no mínimo 3 caracteres")
                
                .MaximumLength(50)
                .WithMessage("Nome deve ter no máximo 50 caracteres");
                
            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome não pode ser nulo ou vazio")

                .EmailAddress()
                .WithMessage("Email inválido");
            
             RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha não pode ser nulo ou vazio")

                .MinimumLength(6)
                .WithMessage("Senha deve ter mínimo de 6 caracteres")
                
                .MaximumLength(15)
                .WithMessage("Senha deve ter máximo de 15 caracteres");
        }
    }
}