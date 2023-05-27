using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IRequestService
{
    public Task CreateRequestAsync(
        CreateRequestModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateRequestAsync(
        UpdateRequestModel model,
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

    public Task<IEnumerable<RequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    );
}