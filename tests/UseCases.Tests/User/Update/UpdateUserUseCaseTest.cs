using CommonTestsUtilities.Entities;
using CommonTestsUtilities.LoggedUser;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using FluentAssertions;
using MyRecipes.Application.UseCases.User.Update;
using MyRecipes.Exceptions;
using MyRecipes.Exceptions.ExceptionsBase;

namespace UseCases.Tests.User.Update;
public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        user.Name.Should().Be(request.Name);
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesExceptions.NAME_EMPTY));

        user.Name.Should().NotBe(request.Name);
        user.Email.Should().NotBe(request.Email);
    }


    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user, request.Email);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 && e.ErrorMessages.Contains(ResourceMessagesExceptions.EMAIL_ALREADY_REGISTERED));

        user.Name.Should().NotBe(request.Name);
        user.Email.Should().NotBe(request.Email);
    }

    private static UpdateUserUseCase CreateUseCase(MyRecipes.Domain.Entities.User user, string? email = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
        var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrEmpty(email))
            readOnlyRepositoryBuilder.ExistsActiveUserWithEmail(email);

        return new UpdateUserUseCase(loggedUser, updateOnlyRepository, readOnlyRepositoryBuilder.Build(), unitOfWork);
    }
}
