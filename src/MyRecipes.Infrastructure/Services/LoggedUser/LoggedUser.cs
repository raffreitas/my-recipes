﻿using Microsoft.EntityFrameworkCore;
using MyRecipes.Domain.Entities;
using MyRecipes.Domain.Security.Tokens;
using MyRecipes.Domain.Services.LoggedUser;
using MyRecipes.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyRecipes.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly MyRecipesDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(MyRecipesDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext.Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);

    }
}
