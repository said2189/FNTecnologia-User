using Application.Commands;
using FluentValidation;

namespace Application.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => role == "Usuario" || role == "Admin")
                .WithMessage("El rol debe ser 'Usuario' o 'Admin'.");
        }
    }
}
