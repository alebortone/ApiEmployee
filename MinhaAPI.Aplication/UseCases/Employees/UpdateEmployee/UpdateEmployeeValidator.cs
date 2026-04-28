using FluentValidation;

namespace MinhaAPI.Aplication.UseCases.Employees.UpdateEmployee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeCommand>
    {

        public UpdateEmployeeValidator() 
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.age)
                .GreaterThan(15);

            RuleFor(x => x.email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
