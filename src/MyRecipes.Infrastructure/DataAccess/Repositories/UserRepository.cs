using Microsoft.EntityFrameworkCore;
using MyRecipes.Domain.Entities;
using MyRecipes.Domain.Repositories.User;

namespace MyRecipes.Infrastructure.DataAccess.Repositories;

internal class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly MyRecipesDbContext _dbContext;

    public UserRepository(MyRecipesDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistsActiveUserWithWithEmail(string email)
        => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
}
