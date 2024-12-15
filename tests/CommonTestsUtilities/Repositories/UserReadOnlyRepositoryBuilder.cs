using Moq;
using MyRecipes.Domain.Repositories.User;

namespace CommonTestsUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

    public void ExistsActiveUserWithEmail(string email)
    {
        _repository.Setup(repository => repository.ExistsActiveUserWithWithEmail(email)).ReturnsAsync(true);
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}
