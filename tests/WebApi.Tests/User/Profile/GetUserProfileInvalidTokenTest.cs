using CommonTestsUtilities.Tokens;
using FluentAssertions;
using System.Net;

namespace WebApi.Tests.User.Profile;
public class GetUserProfileInvalidTokenTest : MyRecipesClassFixture
{
    private readonly string _method = "users";

    public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var response = await DoGet(_method, "invalidToken");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var response = await DoGet(_method, string.Empty);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
        var response = await DoGet(_method, token);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
