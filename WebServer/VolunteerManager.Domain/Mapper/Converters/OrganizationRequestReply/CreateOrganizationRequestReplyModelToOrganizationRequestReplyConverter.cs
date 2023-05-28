using AutoMapper;
using VolunteerManager.Data.Entities;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Mapper.Converters.OrganizationRequest;

internal class CreateOrganizationRequestReplyModelToOrganizationRequestReplyConverter
    : ITypeConverter<CreateOrganizationRequestReplyModel, Data.Entities.OrganizationRequestReply>
{
    public Data.Entities.OrganizationRequestReply Convert(
        CreateOrganizationRequestReplyModel source,
        OrganizationRequestReply destination,
        ResolutionContext context
    ) => new()
    {
        Status = source.Status,
        OrganizationRequestId = source.RequestId
    };
}