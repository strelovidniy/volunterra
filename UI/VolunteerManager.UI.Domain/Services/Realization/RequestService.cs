using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class RequestService : IRequestService
{
    public Task CreateRequestAsync(
        CreateRequestModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task UpdateRequestAsync(
        UpdateRequestModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task DeleteRequestAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task<IEnumerable<RequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();
}