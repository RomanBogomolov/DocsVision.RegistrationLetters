using FluentValidation;

namespace DocsVision.RegistrationLetters.Model.Validators
{
    public class UserEmailValidator : AbstractValidator<string>
    {
        public UserEmailValidator()
        {
            RuleFor(x => x)
                .EmailAddress()
                .WithMessage("Невалидный адрес");
        }
    }
}