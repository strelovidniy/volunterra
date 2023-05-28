using AutoMapper;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Mapper.Converters.Organization;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Profiles;

internal class OrganizationMapperProfile : Profile
{
    public OrganizationMapperProfile()
    {
        CreateMap<CreateOrganizationModel, Organization>()
            .ConvertUsing(new CreateOrganizationModelToOrganizationConverter());

        CreateMap<Organization, OrganizationView>().ConvertUsing(new OrganizationToOrganizationViewConverter());
        CreateMap<Skill, SkillView>();
    }
}