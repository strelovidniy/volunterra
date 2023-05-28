using AutoMapper;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Converters.OrganizationRequestReply;

internal class OrganizationRequestReplyToOrganizationRequestReplyViewConverter
    : ITypeConverter<Data.Entities.OrganizationRequestReply, OrganizationRequestReplyView>
{
    public OrganizationRequestReplyView Convert(
        Data.Entities.OrganizationRequestReply organizationRequest,
        OrganizationRequestReplyView organizationRequestView,
        ResolutionContext context
    ) => new()
    {
        Status = organizationRequest.Status,
        RequestDate = organizationRequest.CreatedAt,
        RequestUpdatedAt = organizationRequest.UpdatedAt,
        Id = organizationRequest.Id,
        CreatedBy = context.Mapper.Map<UserView>(organizationRequest.User)
    };
}