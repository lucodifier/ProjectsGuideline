using FluentValidation.Results;
using System;

namespace Guideline.Application.ViewModels
{
    public class ValidationResultViewModel
    {
        public  Guid Id { get; set; }
        public  ValidationResult Validation { get; set; }

        public ValidationResultViewModel(ValidationResult validationResult)
        {
            Validation = validationResult;
        }
    }
}
