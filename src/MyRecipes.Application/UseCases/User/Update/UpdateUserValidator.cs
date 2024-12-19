using FluentValidation;
using MyRecipes.Communication.Requests;
using MyRecipes.Exceptions;

namespace MyRecipes.Application.UseCases.User.Update;
internal class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.NAME_EMPTY);

        When(request => !string.IsNullOrWhiteSpace(request.Email), () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
        });
    }
}
