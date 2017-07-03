using System.Linq;
using DocsVision.RegistrationLetters.Model.Validators;
using FluentValidation;

namespace DocsVision.RegistrationLetters.Api.Models.Validators
{
    public class CompositeMessageEmailsValidator : AbstractValidator<CompositeMessageEmails>
    {
        public CompositeMessageEmailsValidator()
        {
            RuleFor(x => x.Message)
                .SetValidator(new MessageValidator());

            RuleFor(x => x.Emails)
                .SetCollectionValidator(new UserEmailValidator());
        }
    }
}