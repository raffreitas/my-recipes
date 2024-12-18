using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MyRecipes.Communication.Responses;
using MyRecipes.Domain.Repositories.User;
using MyRecipes.Domain.Security.Tokens;
using MyRecipes.Exceptions;
using MyRecipes.Exceptions.ExceptionsBase;

namespace MyRecipes.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator,
                                   IUserReadOnlyRepository userReadOnlyRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var exists = await _userReadOnlyRepository.ExistsActiveUserWithIdentifier(userIdentifier);
            if (!exists)
            {
                throw new MyRecipesException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }
        catch (MyRecipesException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.Message));
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
            {
                TokenIsExpired = true
            });
        }
        catch
        {
            throw new MyRecipesException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authentication))
        {
            throw new MyRecipesException(ResourceMessagesExceptions.NO_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();
    }
}
