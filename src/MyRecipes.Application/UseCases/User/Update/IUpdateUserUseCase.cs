using MyRecipes.Communication.Requests;

namespace MyRecipes.Application.UseCases.User.Update;
public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserJson request);
}
