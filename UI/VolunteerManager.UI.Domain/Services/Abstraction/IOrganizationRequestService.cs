using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.UI.Domain.Services.Abstraction;

public interface IOrganizationRequestService
{
    public Task CreateRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateRequestAsync(
        UpdateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task DeleteRequestAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    );

    public Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<OrganizationRequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    );
}