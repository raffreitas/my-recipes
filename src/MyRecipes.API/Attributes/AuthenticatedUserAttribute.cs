using Microsoft.AspNetCore.Mvc;
using MyRecipes.API.Filters;

namespace MyRecipes.API.Attributes;

internal class AuthenticatedUserAttribute : TypeFilterAttribute<AuthenticatedUserFilter>
{
}
