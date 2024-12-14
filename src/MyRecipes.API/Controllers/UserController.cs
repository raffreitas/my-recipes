using Microsoft.AspNetCore.Mvc;
using MyRecipes.Application.UseCases.User.Register;
using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;

namespace MyRecipes.API.Controllers;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase,
                                              [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
