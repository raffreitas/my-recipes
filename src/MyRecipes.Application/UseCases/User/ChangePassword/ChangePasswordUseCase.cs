using FluentValidation.Results;
using MyRecipes.Communication.Requests;
using MyRecipes.Domain.Repositories;
using MyRecipes.Domain.Repositories.User;
using MyRecipes.Domain.Security.Cryptography;
using MyRecipes.Domain.Services.LoggedUser;
using MyRecipes.Exceptions;
using MyRecipes.Exceptions.ExceptionsBase;

namespace MyRecipes.Application.UseCases.User.ChangePassword;
internal class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(ILoggedUser loggedUser,
                                 IUserUpdateOnlyRepository repository,
                                 IUnitOfWork unitOfWork,
                                 IPasswordEncripter passwordEncripter)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.User();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var result = new ChangePasswordUseCaseValidator().Validate(request);

        var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);

        if (!currentPasswordEncripted.Equals(loggedUser.Password))
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesExceptions.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

        if (!result.IsValid)
            throw new ErrorOnValidationException([.. result.Errors.Select(e => e.ErrorMessage)]);

    }
}
