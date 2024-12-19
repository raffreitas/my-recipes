using FluentValidation;
using MyRecipes.Application.SharedValidators;
using MyRecipes.Communication.Requests;

namespace MyRecipes.Application.UseCases.User.ChangePassword;
internal class ChangePasswordUseCaseValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordUseCaseValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}
