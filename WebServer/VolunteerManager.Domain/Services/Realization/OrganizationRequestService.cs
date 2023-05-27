using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Services.Realization;

public class OrganizationRequestService : IOrganizationRequestService
{
    private readonly IRepository<OrganizationRequest> _organizationRepository;
    private readonly IMapper _mapper;

    public OrganizationRequestService(
        IRepository<OrganizationRequest> organizationRepository,
        IMapper mapper
    )
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

    public async Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default) 
    {
        await _organizationRepository.AddAsync(
            _mapper.Map<OrganizationRequest>(model),
            cancellationToken
        );

        await _organizationRepository.SaveChangesAsync(cancellationToken);
        
        //TODO: add send email for organization and controller method
    }
}