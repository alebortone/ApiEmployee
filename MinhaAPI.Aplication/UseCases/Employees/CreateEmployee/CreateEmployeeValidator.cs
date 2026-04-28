using FluentValidation;

namespace MinhaAPI.Aplication.UseCases.Employees.CreateEmployee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(x => x.age)
                .GreaterThan(15);

            RuleFor(x => x.email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.password)
                .MinimumLength(6);
        }
    }
}
