﻿using Bogus;
using MyRecipes.Communication.Enums;
using MyRecipes.Communication.Requests;

namespace CommonTestsUtilities.Requests;
public class RequestRecipeJsonBuilder
{
    public static RequestRecipeJson Build()
    {
        var step = 1;

        return new Faker<RequestRecipeJson>()
            .RuleFor(r => r.Title, f => f.Lorem.Word())
            .RuleFor(r => r.CookingTime, f => f.PickRandom<CookingTime>())
            .RuleFor(r => r.Difficulty, f => f.PickRandom<Difficulty>())
            .RuleFor(r => r.Ingredients, f => f.Make(3, () => f.Commerce.ProductName()))
            .RuleFor(r => r.DishType, f => f.Make(3, () => f.PickRandom<DishType>()))
            .RuleFor(r => r.Instructions, f => f.Make(3, () => new RequestInstructionsJson
            {
                Text = f.Lorem.Paragraph(),
                Step = step++
            }));
    }
}
