using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IRequestService
{
    public Task CreateRequestAsync(
        CreateOrganizationInvocationReplyModel model,
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