using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IOrganizationRequestService
{
    public Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task UploadRequestImageAsync(
        byte[] avatar,
        CancellationToken cancellationToken = default
    );

    public Task<OrganizationRequestView?> GetOrganizationRequestAsync(
        Guid organizationRequestId,
        CancellationToken cancellationToken = default
    );

    public Task<List<string>> GetLocationsAsync(
        CancellationToken cancellationToken = default
    );
}