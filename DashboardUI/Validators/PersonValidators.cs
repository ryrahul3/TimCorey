using FluentValidation;
using ModelLibrary;
using System;
using System.Linq;

namespace DashboardUI.Validators
{
    public class PersonValidators : AbstractValidator<PersonModel>
    {
        public PersonValidators()
        {
            RuleFor(p => p.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Please provide a valid {PropertyName} length ({TotalLength})")
                .Must(BeValidName).WithMessage("{PropertyName} Contains invalid charcter");

            RuleFor(p => p.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Please provide a valid {PropertyName} length ({TotalLength})")
                .Must(BeValidName).WithMessage("{PropertyName} Contains invalid charcter");

            RuleFor(p => p.DateOfBirth)
                .Must(BeValidAge).WithMessage("Invalid {PropertyName}");

        }

        protected bool BeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(char.IsLetter);
        }

        protected bool BeValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dob = date.Year;
            return (dob <= currentYear && dob > (currentYear - 120));
        }
    }
}
