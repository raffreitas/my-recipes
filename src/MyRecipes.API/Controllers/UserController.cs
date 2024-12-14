using Microsoft.AspNetCore.Mvc;
using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;

namespace MyRecipes.API.Controllers;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {
        return Created();
    }
}
