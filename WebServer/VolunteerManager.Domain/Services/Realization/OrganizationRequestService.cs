using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;
using VolunteerManager.Domain.Models.ViewModels;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Realization;

public class OrganizationRequestService : IOrganizationRequestService
{
    private readonly IRepository<OrganizationRequest> _organizationRequestRepository;
    private readonly IRepository<Organization> _organizationRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public OrganizationRequestService(
        IRepository<OrganizationRequest> organizationRequestRepository,
        IMapper mapper,
        IRepository<Organization> organizationRepository,
        IEmailService emailService
    )
    {
        _organizationRequestRepository = organizationRequestRepository;
        _mapper = mapper;
        _organizationRepository = organizationRepository;
        _emailService = emailService;
    }

    public async Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default)
    {
        var aaa = _mapper.Map<OrganizationRequest>(model);
        await _organizationRequestRepository.AddAsync(
          aaa,
            cancellationToken
        );

        await _organizationRequestRepository.SaveChangesAsync(cancellationToken);

        var organization = await _organizationRepository.Query().FirstOrDefaultAsync(x=>x.Id == aaa.OrganizationId, cancellationToken: cancellationToken);

        if (organization == null)
        {
            throw new ArgumentException();
        }

        await _emailService.SendEmailAsync(
            organization.GoogleEmail,
            EmailSubject.CreateAccount,
            new NewApplicantEmailViewModel("https://volunteer-manager.azurewebsites.net"),
            cancellationToken: cancellationToken
        );
    }

    public async Task<OrganizationRequestView?> GetOrganizationRequestAsync(Guid organizationRequestId, CancellationToken cancellationToken = default)
    {
        var organizationRequest = await _organizationRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == organizationRequestId, cancellationToken: cancellationToken);

        if (organizationRequest == null)
        {
            throw new ArgumentException();
        }

        return _mapper.Map<OrganizationRequestView>(organizationRequest);
    }
}