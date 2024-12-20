﻿using CommonTestsUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.User.Profile;
public class GetUserProfileTest : MyRecipesClassFixture
{
    private readonly string _method = "users";

    private readonly string _name;
    private readonly string _email;
    private readonly Guid _userIdentifier;

    public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _name = factory.GetName();
        _email = factory.GetEmail();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet(_method, token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_name);
        responseData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_email);
    }
}