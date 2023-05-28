using AutoMapper;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Mapper.Converters.OrganizationRequestReply;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Profiles;

internal class OrganizationRequestReplyMapperProfile : Profile
{
    public OrganizationRequestReplyMapperProfile()
    {
        CreateMap<CreateOrganizationRequestReplyModel, OrganizationRequestReply>()
            .ConvertUsing(new CreateOrganizationRequestReplyModelToOrganizationRequestReplyConverter());

        CreateMap<OrganizationRequestReply, OrganizationRequestReplyView>()
            .ConvertUsing(new OrganizationRequestReplyToOrganizationRequestReplyViewConverter());
    }
}