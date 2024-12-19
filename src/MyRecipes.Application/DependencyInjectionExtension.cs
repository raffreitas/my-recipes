﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Application.Services.AutoMapper;
using MyRecipes.Application.Services.Cryptography;
using MyRecipes.Application.UseCases.Login.DoLogin;
using MyRecipes.Application.UseCases.User.Profile;
using MyRecipes.Application.UseCases.User.Register;
using MyRecipes.Application.UseCases.User.Update;

namespace MyRecipes.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUseCases(services);
        AddPasswordEncripter(services, configuration);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(options => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile<AutoMapping>();
        }).CreateMapper());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
    }

    private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");

        services.AddScoped(options => new PasswordEncripter(additionalKey!));
    }
}
