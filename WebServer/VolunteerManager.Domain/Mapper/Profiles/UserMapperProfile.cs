using AutoMapper;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Mapper.Converters.User;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Profiles;

internal class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<CreateUserModel, User>().ConvertUsing(new CreateUserModelToUserConverter());

        CreateMap<User, UserView>().ConvertUsing(new UserToUserViewConverter());
    }
}