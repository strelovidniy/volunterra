using AutoMapper;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Mapper.Converters.OrganizationRequestReply;

internal class CreateOrganizationRequestReplyModelToOrganizationRequestReplyConverter
    : ITypeConverter<CreateOrganizationRequestReplyModel, Data.Entities.OrganizationRequestReply>
{
    public Data.Entities.OrganizationRequestReply Convert(
        CreateOrganizationRequestReplyModel source,
        Data.Entities.OrganizationRequestReply destination,
        ResolutionContext context
    ) => new()
    {
        Status = source.Status,
        OrganizationRequestId = source.RequestId
    };
}