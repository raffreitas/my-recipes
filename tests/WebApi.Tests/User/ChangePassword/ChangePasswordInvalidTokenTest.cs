using CommonTestsUtilities.Tokens;
using FluentAssertions;
using MyRecipes.Communication.Requests;
using System.Net;

namespace WebApi.Tests.User.ChangePassword;
public class ChangePasswordInvalidTokenTest : MyRecipesClassFixture
{
    private const string METHOD = "users/change-password";
    public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = new RequestChangePasswordJson();

        var response = await DoPut(METHOD, request, token: "INVALID_TOKEN");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var request = new RequestChangePasswordJson(); ;
        var response = await DoPut(METHOD, request, token: string.Empty);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var request = new RequestChangePasswordJson();

        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var response = await DoPut(METHOD, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
