using CommonTestsUtilities.Requests;
using FluentAssertions;
using MyRecipes.Application.UseCases.User.ChangePassword;
using MyRecipes.Exceptions;

namespace Validators.Tests.User.ChangePassword;
public class ChangePasswordValidationTest
{
    [Fact]
    public void Success()
    {
        var validator = new ChangePasswordUseCaseValidator();
        var request = RequestChangePasswordJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var validator = new ChangePasswordUseCaseValidator();
        var request = RequestChangePasswordJsonBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.EndsWith(ResourceMessagesExceptions.INVALID_PASSWORD));
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new ChangePasswordUseCaseValidator();
        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = string.Empty;    

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(e => e.ErrorMessage.EndsWith(ResourceMessagesExceptions.PASSWORD_EMPTY));
    }
}
