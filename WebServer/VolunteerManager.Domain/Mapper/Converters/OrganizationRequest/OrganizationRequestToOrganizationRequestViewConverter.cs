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
        ServedCount = organizationRequest.ServedCount,
        Request = context.Mapper.Map<RequestView>(organizationRequest.Request)
    };
}