using FluentValidation;

namespace DocsVision.RegistrationLetters.Model.Validators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.Text)
                .Length(3, 4000)
                .WithMessage("Сообщение должно содержать не менее 3 и не более 4000 символов.")
                .NotNull()
                .WithMessage("Должно быть указано сообщение");

            RuleFor(x => x.Theme)
                .Length(3, 30)
                .WithMessage("Тема должна содержать не менее 3 и не более 30 символов.")
                .NotNull()
                .WithMessage("Должна быть указана тема");
               
        }
    }
}