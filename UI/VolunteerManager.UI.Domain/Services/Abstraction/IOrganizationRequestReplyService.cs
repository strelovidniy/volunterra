using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.UI.Domain.Services.Abstraction;

public interface IOrganizationRequestReplyService
{
    public Task CreateRequestReplyAsync(
        CreateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateRequestReplyAsync(
        UpdateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    );

    public Task DeleteRequestReplyAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    );
}