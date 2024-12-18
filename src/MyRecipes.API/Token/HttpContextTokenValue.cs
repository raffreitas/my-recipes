﻿using MyRecipes.Domain.Security.Tokens;

namespace MyRecipes.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextTokenValue(IHttpContextAccessor contextAccessor) => _contextAccessor = contextAccessor;


    public string Value()
    {
        var authentication = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authentication["Bearer ".Length..].Trim();
    }
}
