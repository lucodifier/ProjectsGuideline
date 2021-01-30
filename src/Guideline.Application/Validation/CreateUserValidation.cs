using FluentValidation;
using Guideline.Application.ViewModels;

namespace Guideline.Application.Validation
{
    public class CreateUserValidation : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidation()
        {
            RuleFor(v => v.Name)
               .NotEmpty().WithMessage("Por favor preencha o nome");

            RuleFor(v => v.Name)
               .Length(2, 150).WithMessage("O nome precisa ter de 2 a 150 caracteres")
               .When(v => !string.IsNullOrEmpty(v.Name));

            RuleFor(v => v.Login)
               .NotEmpty().WithMessage("Por favor preencha o login");

            RuleFor(v => v.Login)
               .Length(4, 30).WithMessage("O login precisa ter de 4 a 30 caracteres")
               .When(v => !string.IsNullOrEmpty(v.Login));

            RuleFor(v => v.Pass)
               .NotEmpty().WithMessage("Por favor preencha a senha");

            RuleFor(v => v.Pass)
               .Length(4, 10).WithMessage("A senha precisa ter de 4 a 10 caracteres")
               .When(v => !string.IsNullOrEmpty(v.Pass));

            RuleFor(c => c.Email)
               .NotEmpty()
               .EmailAddress();
        }
    }
}
