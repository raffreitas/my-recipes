using MyRecipes.Domain.Security.Cryptography;
using MyRecipes.Infrastructure.Security.Cryptography;

namespace CommonTestsUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new Sha512Encripter("ABC1234");
}
