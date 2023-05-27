using AutoMapper;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Converters.Organization;

internal class OrganizationToOrganizationViewConverter : ITypeConverter<Data.Entities.Organization, OrganizationView>
{
    public OrganizationView Convert(
        Data.Entities.Organization organization,
        OrganizationView organizationView,
        ResolutionContext context
    ) => new()
    {
        Id = organization.Id,
        Name = organization.Name,
        Description = organization.Description,
        ImageDataUrl = organization.ImageDataUrl,
        GoogleEmail = organization.GoogleEmail,
        Users = context.Mapper.Map<IEnumerable<UserView>>(organization.Users),
        OrganizationRequests
            = context.Mapper.Map<IEnumerable<OrganizationRequestView>>(organization.OrganizationRequests)
    };
}