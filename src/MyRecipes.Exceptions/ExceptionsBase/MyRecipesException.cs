namespace MyRecipes.Exceptions.ExceptionsBase;

public class MyRecipesException : SystemException
{
    public MyRecipesException(string message) : base(message) { }
}
