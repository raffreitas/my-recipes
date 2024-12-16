using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;

namespace MyRecipes.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
