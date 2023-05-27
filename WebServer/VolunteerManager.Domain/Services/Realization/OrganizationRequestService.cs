using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Services.Realization;

public class OrganizationRequestService : IOrganizationRequestService
{
    private readonly IRepository<OrganizationRequest> _organizationRequestRepository;
    private readonly IRepository<Organization> _organizationRepository;
    private readonly IMapper _mapper;

    public OrganizationRequestService(
        IRepository<OrganizationRequest> organizationRequestRepository,
        IMapper mapper,
        IRepository<Organization> organizationRepository
    )
    {
        _organizationRequestRepository = organizationRequestRepository;
        _mapper = mapper;
        _organizationRepository = organizationRepository;
    }

    public async Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default) 
    {
        await _organizationRequestRepository.AddAsync(
            _mapper.Map<OrganizationRequest>(model),
            cancellationToken
        );

        await _organizationRequestRepository.SaveChangesAsync(cancellationToken);
        
    }
}