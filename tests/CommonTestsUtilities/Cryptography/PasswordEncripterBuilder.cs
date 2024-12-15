using MyRecipes.Application.Services.Cryptography;

namespace CommonTestsUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static PasswordEncripter Build() => new("ABC1234");
}
