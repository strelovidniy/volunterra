using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums.RichEnums;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Domain.Models.ViewModels;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Settings.Abstraction;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Realization;

public class OrganizationRequestService : IOrganizationRequestService
{
    private readonly IRepository<OrganizationRequest> _organizationRequestRepository;
    private readonly IRepository<Organization> _organizationRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailService _emailService;
    private readonly IUrlSettings _urlSettings;
    private readonly IMapper _mapper;

    public OrganizationRequestService(
        IRepository<OrganizationRequest> organizationRequestRepository,
        IMapper mapper,
        IRepository<Organization> organizationRepository,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor,
        IUrlSettings urlSettings
    )
    {
        _organizationRequestRepository = organizationRequestRepository;
        _mapper = mapper;
        _organizationRepository = organizationRepository;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _urlSettings = urlSettings;
    }

    public async Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    )
    {
        var organizationRequest = _mapper.Map<OrganizationRequest>(model);

        await _organizationRequestRepository.AddAsync(
            organizationRequest,
            cancellationToken
        );

        await _organizationRequestRepository.SaveChangesAsync(cancellationToken);

        var organization = await _organizationRepository.Query()
            .FirstOrDefaultAsync(x => x.Id == organizationRequest.OrganizationId, cancellationToken);

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

    public async Task UploadRequestImageAsync(byte[] avatar, CancellationToken cancellationToken = default)
    {
        var organizationRequest = _organizationRequestRepository
            .Query()
            .FirstOrDefault(user => user.Id == _httpContextAccessor.GetCurrentUserId());

        var avatarName = organizationRequest!.Id.ToString();

        var combine = Path.Combine(Directory.GetCurrentDirectory(),
            "Files",
            "Requests");

        var filePath = Path.Combine(
            combine,
            avatarName
        );

        if (!Directory.Exists(combine))
        {
            Directory.CreateDirectory(combine);
        }

        await File.WriteAllBytesAsync(filePath, avatar, cancellationToken);

        organizationRequest.ImageDataUrl = _urlSettings.WebApiUrl + "api/v1/organizationRequests/images/" + avatarName;

        await _organizationRequestRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<OrganizationRequestView?> GetOrganizationRequestAsync(
        Guid organizationRequestId,
        CancellationToken cancellationToken = default
    )
    {
        var organizationRequest = await _organizationRequestRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == organizationRequestId, cancellationToken);

        if (organizationRequest == null)
        {
            throw new ArgumentException();
        }

        return _mapper.Map<OrganizationRequestView>(organizationRequest);
    }

    public async Task<List<string>> GetLocationsAsync(CancellationToken cancellationToken = default)
    {
        var organizationResponse = await _organizationRequestRepository
            .Query().Select(x => x.Location).DistinctBy(x => x).ToListAsync(cancellationToken);

        return organizationResponse;
    }
}