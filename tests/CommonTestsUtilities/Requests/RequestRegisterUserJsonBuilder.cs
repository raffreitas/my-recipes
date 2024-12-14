using Bogus;
using MyRecipes.Communication.Requests;

namespace CommonTestsUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build(int passwordLength = 10)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(request => request.Name, (f) => f.Person.FirstName)
            .RuleFor(request => request.Email, (f, r) => f.Internet.Email(r.Name))
            .RuleFor(request => request.Password, (f) => f.Internet.Password(passwordLength));
    }
}
