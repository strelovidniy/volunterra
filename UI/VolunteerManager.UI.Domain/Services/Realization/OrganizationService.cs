using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class OrganizationService : IOrganizationService
{
    public Task CreateOrganizationAsync(
        CreateOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task UpdateOrganizationAsync(
        UpdateOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task DeleteOrganizationAsync(
        Guid organizationId,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task InviteUserToOrganizationAsync(
        InviteUserToOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task RemoveUserFromOrganizationAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task AcceptInviteAsync(
        Guid invitationToken,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();
}