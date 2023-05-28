using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.Services.Abstraction;

public interface IOrganizationService
{
    public Task CreateOrganizationAsync(
        CreateOrganizationModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateOrganizationAsync(
        UpdateOrganizationModel model,
        CancellationToken cancellationToken = default
    );

    public Task DeleteOrganizationAsync(
        Guid organizationId,
        CancellationToken cancellationToken = default
    );

    public Task InviteUserToOrganizationAsync(
        InviteUserToOrganizationModel model,
        CancellationToken cancellationToken = default
    );

    public Task RemoveUserFromOrganizationAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    public Task AcceptInviteAsync(
        Guid invitationToken,
        CancellationToken cancellationToken = default
    );
}