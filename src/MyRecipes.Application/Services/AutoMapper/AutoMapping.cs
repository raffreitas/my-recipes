using AutoMapper;
using MyRecipes.Communication.Requests;

namespace MyRecipes.Application.Services.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(destination => destination.Password, option => option.Ignore());
    }
}
