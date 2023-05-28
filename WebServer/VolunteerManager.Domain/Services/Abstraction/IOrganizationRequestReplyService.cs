using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IOrganizationRequestReplyService
{
    public Task CreateOrganizationRequestReplyAsync(
        CreateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateOrganizationRequestReplyAsync(
        UpdateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    );

    public Task DeleteOrganizationRequestReplyAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    );
}