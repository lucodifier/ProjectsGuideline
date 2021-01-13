using FluentValidation;
using Guideline.Application.ViewModels;

namespace Guideline.Application.Validation
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserViewModel>
    {
        public UpdateUserValidation()
        {
            RuleFor(c => c.Id)
               .NotEmpty();

            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Por favor preencha o nome")
               .Length(2, 150).WithMessage("O nome precisa ter de 2 a 150 caracteres");

            RuleFor(c => c.Login)
               .NotEmpty().WithMessage("Por favor preencha o login")
               .Length(4, 30).WithMessage("O login precisa ter de 4 a 30 caracteres");

            RuleFor(c => c.Email)
               .NotEmpty()
               .EmailAddress();
        }
    }
}
