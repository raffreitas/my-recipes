using MyRecipes.Communication.Responses;

namespace MyRecipes.Application.UseCases.User.Profile;
public interface IGetUserProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}
