using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.UI.Domain.Services.Abstraction;

public interface IOrganizationRequestInvocationService
{
    public Task CreateOrganizationRequestInvocationAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateOrganizationRequestInvocationAsync(
        UpdateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task DeleteOrganizationRequestInvocationAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    );

    public Task CreateOrganizationInvocationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<OrganizationRequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    );
}