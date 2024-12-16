using Microsoft.AspNetCore.Mvc;
using MyRecipes.Application.UseCases.Login.DoLogin;
using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;

namespace MyRecipes.API.Controllers;

[Route("login")]
public class LoginController : MyRecipesBaseController
{
    [HttpPost]
    [ProducesResponseType<ResponseRegisteredUserJson>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorJson>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase userCase,
        [FromBody] RequestLoginJson request)
    {
        var response = await userCase.Execute(request);

        return Ok(response);
    }
}
