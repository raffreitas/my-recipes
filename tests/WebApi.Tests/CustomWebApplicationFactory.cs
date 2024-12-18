using CommonTestsUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Infrastructure.DataAccess;

namespace WebApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private MyRecipes.Domain.Entities.User _user = default!;
    private string _password = string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<MyRecipesDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<MyRecipesDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<MyRecipesDbContext>();
                dbContext.Database.EnsureDeleted();

                StartDatabase(dbContext);
            }); ;
    }

    public string GetName() => _user.Name;
    public string GetEmail() => _user.Email;
    public string GetPassword() => _password;
    public Guid GetUserIdentifier() => _user.UserIdentifier;

    private void StartDatabase(MyRecipesDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();
        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}
