using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.Services.Realization;

public class OrganizationService : IOrganizationService
{
    private readonly IRepository<Organization> _organizationRepository;
    private readonly IMapper _mapper;

    public OrganizationService(
        IRepository<Organization> organizationRepository,
        IMapper mapper
    )
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

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

    public async Task CreateOrganizationAsync(
        CreateOrganizationModel model,
        CancellationToken cancellationToken = default
    )
    {
        await _organizationRepository.AddAsync(
            _mapper.Map<Organization>(model),
            cancellationToken
        );

        await _organizationRepository.SaveChangesAsync(cancellationToken);
    }

    public Task InviteUserToOrganizationAsync(
        InviteUserToOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException(); //TODO: Invite non-existing user and also existing without org
}