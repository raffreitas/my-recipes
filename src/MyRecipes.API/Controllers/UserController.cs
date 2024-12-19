using Microsoft.AspNetCore.Mvc;
using MyRecipes.API.Attributes;
using MyRecipes.Application.UseCases.User.ChangePassword;
using MyRecipes.Application.UseCases.User.Profile;
using MyRecipes.Application.UseCases.User.Register;
using MyRecipes.Application.UseCases.User.Update;
using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;

namespace MyRecipes.API.Controllers;

[Route("users")]
public class UserController : MyRecipesBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase,
                                              [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [AuthenticatedUser]
    [ProducesResponseType<ResponseUserProfileJson>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase userCase)
    {
        var result = await userCase.Execute();

        return Ok(result);
    }

    [HttpPut]
    [AuthenticatedUser]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseErrorJson>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [HttpPut("change-password")]
    [AuthenticatedUser]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ResponseErrorJson>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }
}
