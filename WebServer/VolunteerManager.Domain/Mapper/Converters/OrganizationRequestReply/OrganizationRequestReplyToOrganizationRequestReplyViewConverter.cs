using AutoMapper;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Mapper.Converters.OrganizationRequest;

internal class OrganizationRequestReplyToOrganizationRequestReplyViewConverter
    : ITypeConverter<Data.Entities.OrganizationRequestReply, OrganizationRequestReplyView>
{
    public OrganizationRequestReplyView Convert(
        Data.Entities.OrganizationRequestReply organizationRequest,
        OrganizationRequestReplyView organizationRequestView,
        ResolutionContext context
    ) => new()
    {
        Status = organizationRequestView.Status
    };
}