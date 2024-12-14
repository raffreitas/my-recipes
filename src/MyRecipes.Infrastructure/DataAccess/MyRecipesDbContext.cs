using Microsoft.EntityFrameworkCore;
using MyRecipes.Domain.Entities;

namespace MyRecipes.Infrastructure.DataAccess;

public class MyRecipesDbContext : DbContext
{
    public MyRecipesDbContext(DbContextOptions<MyRecipesDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipesDbContext).Assembly);
    }
}
