using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.Services.Realization;

internal class OrganizationService : IOrganizationService
{
    private readonly IRepository<Organization> _organizationRepository;

    public OrganizationService(
        IRepository<Organization> organizationRepository
    ) => _organizationRepository = organizationRepository;

    public Task UpdateOrganizationAsync(
        UpdateOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task DeleteOrganizationAsync(
        Guid organizationId,
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

    public Task CreateOrganizationAsync(
        CreateOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();

    public Task InviteUserToOrganizationAsync(
        InviteUserToOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException(); //TODO: Invite non-existing user and also existing without org
}