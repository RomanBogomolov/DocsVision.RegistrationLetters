using System.Linq;
using DocsVision.RegistrationLetters.Model.Validators;
using FluentValidation;

namespace DocsVision.RegistrationLetters.Api.Models.Validators
{
    public class MessageEmailsInputModelValidator : AbstractValidator<MessageEmailsInputModel>
    {
        public MessageEmailsInputModelValidator()
        {
            RuleFor(x => x.Message)
                .SetValidator(new MessageValidator());

            RuleFor(x => x.Emails)
                .SetCollectionValidator(new UserEmailValidator());
        }
    }
}