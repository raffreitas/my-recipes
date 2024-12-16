namespace MyRecipes.Exceptions.ExceptionsBase;

public class InvalidLoginException : MyRecipesException
{
    public InvalidLoginException(): base(ResourceMessagesExceptions.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}
