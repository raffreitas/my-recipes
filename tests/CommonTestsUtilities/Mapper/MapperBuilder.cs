using AutoMapper;
using MyRecipes.Application.Services.AutoMapper;

namespace CommonTestsUtilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        return new MapperConfiguration(options =>
        {
            options.AddProfile<AutoMapping>();
        }).CreateMapper();
    }
}
