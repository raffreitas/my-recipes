using Microsoft.AspNetCore.Mvc;
using MyRecipes.API.Filters;

namespace MyRecipes.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal class AuthenticatedUserAttribute : TypeFilterAttribute<AuthenticatedUserFilter>
{
}
