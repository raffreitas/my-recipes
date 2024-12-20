using CommonTestsUtilities.Cryptography;
using CommonTestsUtilities.Entities;
using CommonTestsUtilities.LoggedUser;
using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using FluentAssertions;
using MyRecipes.Application.UseCases.User.ChangePassword;
using MyRecipes.Communication.Requests;
using MyRecipes.Exceptions;
using MyRecipes.Exceptions.ExceptionsBase;

namespace UseCases.Tests.User.ChangePassword;
public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = password;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        var passwordEncripter = PasswordEncripterBuilder.Build();
        user.Password.Should().Be(passwordEncripter.Encrypt(request.NewPassword));
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var (user, password) = UserBuilder.Build();
        var request = new RequestChangePasswordJson
        {
            Password = password,
            NewPassword = string.Empty
        };

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 &&
                e.ErrorMessages.Contains(ResourceMessagesExceptions.PASSWORD_EMPTY));

        var passwordEncripter = PasswordEncripterBuilder.Build();
        user.Password.Should().Be(passwordEncripter.Encrypt(password));
    }

    [Fact]
    public async Task Error_CurrentPassword_Differed()
    {
        var (user, password) = UserBuilder.Build();
        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 &&
            e.ErrorMessages.Contains(ResourceMessagesExceptions.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

        var passwordEncripter = PasswordEncripterBuilder.Build();
        user.Password.Should().Be(passwordEncripter.Encrypt(password));
    }

    private static ChangePasswordUseCase CreateUseCase(MyRecipes.Domain.Entities.User user)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var passwordEncripter = PasswordEncripterBuilder.Build();
        return new ChangePasswordUseCase(loggedUser, userUpdateRepository, unitOfWork, passwordEncripter);
    }
}
