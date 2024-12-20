using CommonTestsUtilities.Requests;
using FluentAssertions;
using MyRecipes.Application.UseCases.Recipe;
using MyRecipes.Communication.Enums;
using MyRecipes.Exceptions;

namespace Validators.Tests.Recipe;
public class RecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Success_Cooking_Time_Null()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = null;

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Success_Difficulty_Null()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = null;

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Invalid_Cooking_Time()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.CookingTime = (CookingTime?)100;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceMessagesExceptions.COOKING_TIME_NOT_SUPPORTED);
    }

    [Fact]
    public void Error_Invalid_Difficulty()
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.Difficulty = (Difficulty?)100;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceMessagesExceptions.DIFFICULTY_LEVEL_NOT_SUPPORTED);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("        ")]
    [InlineData("")]

    public void Error_Empty_Title(string title)
    {
        var validator = new RecipeValidator();
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage == ResourceMessagesExceptions.RECIPE_TITLE_EMPTY);
    }

    [Fact]
    public void Success_DishTypes_Empty()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.DishType.Clear();

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Success_Invalid_DishTypes()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.DishType.Add((DishType)(900));

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.DISH_TYPE_NOT_SUPPORTED));
    }

    [Fact]
    public void Error_Empty_Ingredients()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients.Clear();

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.AT_LEAST_ONE_INGREDIENT));
    }

    [Fact]
    public void Error_Empty_Instructions()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.Clear();

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.AT_LEAST_ONE_INSTRUCTION));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("        ")]
    [InlineData("")]
    public void Error_Empty_Value_Ingredients(string ingredient)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients.Add(ingredient);

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.INGREDIENT_EMPTY));
    }

    [Fact]
    public void Error_Same_Step_Instructions()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Step = request.Instructions.Last().Step;

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.TWO_OR_MORE_INSTRUCTIONS_SAME_ORDER));
    }

    [Fact]
    public void Error_Negative_Step_Instructions()
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Step = -1;

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.NON_NEGATIVE_INSTRUCTION_STEP));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("        ")]
    [InlineData("")]
    public void Error_Empty_Value_Instructions(string instruction)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions.First().Text = instruction;

        var validator = new RecipeValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.INSTRUCTION_EMPTY));
    }
}
