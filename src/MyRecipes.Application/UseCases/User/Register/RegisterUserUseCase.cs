﻿using AutoMapper;
using MyRecipes.Communication.Requests;
using MyRecipes.Communication.Responses;
using MyRecipes.Domain.Repositories;
using MyRecipes.Domain.Repositories.User;
using MyRecipes.Domain.Security.Cryptography;
using MyRecipes.Domain.Security.Tokens;
using MyRecipes.Exceptions;
using MyRecipes.Exceptions.ExceptionsBase;

namespace MyRecipes.Application.UseCases.User.Register;

internal class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository,
                               IUserReadOnlyRepository readOnlyRepository,
                               IMapper mapper,
                               IPasswordEncripter passwordEncripter,
                               IUnitOfWork unitOfWork,
                               IAccessTokenGenerator accessTokenGenerator)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _writeOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailExists = await _readOnlyRepository.ExistsActiveUserWithWithEmail(request.Email);

        if (emailExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_ALREADY_REGISTERED));


        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
