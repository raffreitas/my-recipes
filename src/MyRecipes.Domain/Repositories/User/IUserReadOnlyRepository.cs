namespace MyRecipes.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsActiveUserWithWithEmail(string email);
    public Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier);
    public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
}
