using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.UI.Domain.Services.Abstraction;

public interface IOrganizationRequestService
{
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

    public Task UploadImageAsync(
        byte[] bytes,
        CancellationToken cancellationToken = default
    );

    public Task<OrganizationRequestView?> GetOrganizationRequestAsync(
        Guid organizationRequestId,
        CancellationToken cancellationToken = default
    );
}