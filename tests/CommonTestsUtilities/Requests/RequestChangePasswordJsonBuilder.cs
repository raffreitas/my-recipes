using Bogus;
using MyRecipes.Communication.Requests;

namespace CommonTestsUtilities.Requests;
public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build(int passwordLength = 10)
    {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(r => r.Password, f => f.Internet.Password())
            .RuleFor(r => r.NewPassword, f => f.Internet.Password(passwordLength)); ;
    }
}
