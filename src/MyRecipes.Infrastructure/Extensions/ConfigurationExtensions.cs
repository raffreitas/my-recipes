using Microsoft.Extensions.Configuration;

namespace MyRecipes.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsUnitTestEnvironment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }

    public static string GetDatabaseConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultConnection")!;
    }
}
