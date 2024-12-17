using MyRecipes.Application.Services.Cryptography;
using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;
using MyRecipes.Domain.Repositories.User;
using MyRecipes.Domain.Security.Tokens;
using MyRecipes.Exceptions.ExceptionsBase;

namespace MyRecipes.Application.UseCases.Login.DoLogin;

internal class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
                          PasswordEncripter passwordEncripter,
                          IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = _passwordEncripter.Encrypt(request.Password);

        var user = await _userReadOnlyRepository.GetByEmailAndPassword(request.Email, encryptedPassword) ?? throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
