using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace MyRecipes.API.OpenApi;

internal sealed class OpenApiSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var securitySchema = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "JWT Authorization header using the Bearer scheme."
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    },
                },
                []
            }
        };

        document.SecurityRequirements.Add(securityRequirement);
        document.Components = new OpenApiComponents()
        {
            SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>()
            {
                { "Bearer", securitySchema }
            }
        };
        return Task.CompletedTask;
    }
}
