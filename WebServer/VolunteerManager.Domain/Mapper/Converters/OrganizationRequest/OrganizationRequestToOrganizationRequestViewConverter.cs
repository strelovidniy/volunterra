using AutoMapper;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Converters.OrganizationRequest;

internal class OrganizationRequestToOrganizationRequestViewConverter
    : ITypeConverter<Data.Entities.OrganizationRequest, OrganizationRequestView>
{
    public OrganizationRequestView Convert(
        Data.Entities.OrganizationRequest organizationRequest,
        OrganizationRequestView organizationRequestView,
        ResolutionContext context
    ) => new()
    {
        RequestDate = organizationRequest.CreatedAt,
        RequestUpdatedAt = organizationRequest.UpdatedAt,
        Location = organizationRequest.Location,
        ImageDataUrl = organizationRequest.ImageDataUrl,
        Category = organizationRequest.OrganizationRequestCategory,
        CreatedBy = context.Mapper.Map<UserView>(
            organizationRequest.Organization?.Users?.FirstOrDefault(x => x.IsOrganizationOwner)),
        RequestReplyViews = context.Mapper.Map<List<OrganizationRequestReplyView>>(organizationRequest.RequestReplies),
        Title = organizationRequest.Title,
        Description = organizationRequest.Description,
        Id = organizationRequest.Id
    };
}