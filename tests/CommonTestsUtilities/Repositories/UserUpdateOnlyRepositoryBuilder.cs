using Moq;
using MyRecipes.Domain.Entities;
using MyRecipes.Domain.Repositories.User;

namespace CommonTestsUtilities.Repositories;
public class UserUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUserUpdateOnlyRepository> _repository;

    public UserUpdateOnlyRepositoryBuilder() => _repository = new Mock<IUserUpdateOnlyRepository>();

    public IUserUpdateOnlyRepository Build() => _repository.Object;

    public UserUpdateOnlyRepositoryBuilder GetById(User user)
    {
        _repository.Setup(repository => repository.GetById(It.IsAny<long>())).ReturnsAsync(user);
        return this;
    }
}
