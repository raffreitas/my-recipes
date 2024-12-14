using Microsoft.Extensions.Configuration;

namespace MyRecipes.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string GetDatabaseConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultConnection")!;
    }
}
