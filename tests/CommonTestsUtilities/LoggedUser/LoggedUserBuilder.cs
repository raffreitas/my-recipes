using Moq;
using MyRecipes.Domain.Entities;
using MyRecipes.Domain.Services.LoggedUser;

namespace CommonTestsUtilities.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(x => x.User()).ReturnsAsync(user);

        return mock.Object;
    }
}
