using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;

namespace MyRecipes.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
