using AutoMapper;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Mapper.Converters.OrganizationRequest;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Profiles;

internal class OrganizationRequestMapperProfile : Profile
{
    public OrganizationRequestMapperProfile()
    {
        CreateMap<CreateOrganizationRequestModel, OrganizationRequest>()
            .ConvertUsing(new CreateOrganizationRequestModelToOrganizationRequestConverter());

        CreateMap<OrganizationRequest, OrganizationRequestView>()
            .ConvertUsing(new OrganizationRequestToOrganizationRequestViewConverter());
    }
}