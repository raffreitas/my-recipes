using CommonTestsUtilities.Entities;
using CommonTestsUtilities.LoggedUser;
using CommonTestsUtilities.Mapper;
using FluentAssertions;
using MyRecipes.Application.UseCases.User.Profile;

namespace UseCases.Tests.User.Profile;
public class GetUserProfileUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var response = await useCase.Execute();

        response.Should().NotBeNull();
        response.Name.Should().Be(user.Name);
        response.Email.Should().Be(user.Email);
    }

    private static GetUserProfileUseCase CreateUseCase(MyRecipes.Domain.Entities.User user)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetUserProfileUseCase(loggedUser, mapper);
    }
}
