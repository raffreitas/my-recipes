﻿namespace MyRecipes.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : MyRecipesException
{
    public IList<string> ErrorMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
