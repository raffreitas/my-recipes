using MyRecipes.Domain.Repositories;

namespace MyRecipes.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly MyRecipesDbContext _dbContext;

    public UnitOfWork(MyRecipesDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
