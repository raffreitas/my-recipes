using MyRecipes.API.Converters;
using MyRecipes.API.Filters;
using MyRecipes.API.Middleware;
using MyRecipes.API.OpenApi;
using MyRecipes.API.Token;
using MyRecipes.Application;
using MyRecipes.Domain.Security.Tokens;
using MyRecipes.Infrastructure;
using MyRecipes.Infrastructure.Extensions;
using MyRecipes.Infrastructure.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new StringConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<OpenApiSecuritySchemeTransformer>();
});

builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

await app.RunAsync();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnvironment())
        return;

    var connectionString = builder.Configuration.GetDatabaseConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DatabaseMigration.Migrate(connectionString, serviceScope.ServiceProvider);
}

public partial class Program
{
    protected Program() { }
}
