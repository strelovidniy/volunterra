using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IOrganizationRequestService
{
    public Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    );
}