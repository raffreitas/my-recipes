using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Domain.Repositories;
using MyRecipes.Domain.Repositories.User;
using MyRecipes.Infrastructure.DataAccess;
using MyRecipes.Infrastructure.DataAccess.Repositories;
using MyRecipes.Infrastructure.Extensions;
using System.Reflection;

namespace MyRecipes.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddFluentMigrator(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetDatabaseConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(8, 4));

        services.AddDbContext<MyRecipesDbContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetDatabaseConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
                .AddMySql8()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.All();
        });
    }
}
