using MyRecipes.Domain.Entities;

namespace MyRecipes.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}
