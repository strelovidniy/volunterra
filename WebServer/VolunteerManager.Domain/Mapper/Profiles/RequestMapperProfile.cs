using AutoMapper;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Mapper.Converters.Request;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Profiles;

internal class RequestMapperProfile : Profile
{
    public RequestMapperProfile()
    {
        CreateMap<CreateRequestModel, Request>().ConvertUsing(new CreateRequestModelToRequestConverter());

        CreateMap<Request, RequestView>().ConvertUsing(new RequestToRequestViewConverter());
    }
}