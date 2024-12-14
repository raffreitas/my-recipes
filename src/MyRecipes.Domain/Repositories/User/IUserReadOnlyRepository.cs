namespace MyRecipes.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithWithEmail(string email);
}
