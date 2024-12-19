using MyRecipes.Communication.Requests;

namespace MyRecipes.Application.UseCases.User.ChangePassword;
public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePasswordJson request);
}
