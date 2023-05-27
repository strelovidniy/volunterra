using AutoMapper;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Mapper.Converters.OrganizationRequest;

internal class CreateOrganizationRequestModelToOrganizationRequestConverter
    : ITypeConverter<CreateOrganizationRequestModel, Data.Entities.OrganizationRequest>
{
    public Data.Entities.OrganizationRequest Convert(
        CreateOrganizationRequestModel createOrganizationRequestModel,
        Data.Entities.OrganizationRequest organizationRequest,
        ResolutionContext context
    ) => new()
    {
    };
}