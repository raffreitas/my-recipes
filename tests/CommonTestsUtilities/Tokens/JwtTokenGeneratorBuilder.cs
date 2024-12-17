using MyRecipes.Domain.Security.Tokens;
using MyRecipes.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestsUtilities.Tokens;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "824bb0dc-84dc-4fe9-bfb2-9ee4b14d7376");
}
