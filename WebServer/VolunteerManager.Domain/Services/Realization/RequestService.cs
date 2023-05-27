using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Settings.Abstraction;
using VolunteerManager.Domain.Validators.Runtime;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Realization;

internal class RequestService : IRequestService
{
    private readonly IRepository<Request> _requestRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IUrlSettings _urlSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public RequestService(
        IRepository<Request> requestRepository,
        IRepository<User> userRepository,
        IHttpContextAccessor httpContextAccessor,
        IUrlSettings urlSettings,
        IMapper mapper
    )
    {
        _requestRepository = requestRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _urlSettings = urlSettings;
        _mapper = mapper;
    }

    public async Task CreateRequestAsync(
        CreateRequestModel model,
        CancellationToken cancellationToken = default
    )
    {
        var addedRequest = await _requestRepository.AddAsync(
            _mapper.Map<Request>(model),
            cancellationToken
        );

        string? imageDataUrl = null;

        if (model.ImageData is not null)
        {
            var currentUser = _userRepository
                .Query()
                .FirstOrDefault(user => user.Id == _httpContextAccessor.GetCurrentUserId());

            RuntimeValidator.Assert(currentUser is not null, StatusCode.Unauthorized);

            var imageName = addedRequest.Id.ToString();

            var combine = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Requests");

            var filePath = Path.Combine(combine, imageName);

            if (!Directory.Exists(combine))
            {
                Directory.CreateDirectory(combine);
            }

            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(model.ImageData), cancellationToken);

            imageDataUrl = _urlSettings.WebApiUrl + "api/v1/requests/images/" + imageName;
        }

        addedRequest.ImageDataUrl = imageDataUrl;

        await _requestRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRequestAsync(
        UpdateRequestModel model,
        CancellationToken cancellationToken = default
    )
    {
        var request = _requestRepository
            .Query()
            .FirstOrDefault(request => request.Id == model.RequestId);

        RuntimeValidator.Assert(request is not null, StatusCode.RequestNotFound);

        if (model.Description is not null && model.Description != request!.Description)
        {
            request.Description = model.Description;
            request.UpdatedAt = DateTime.UtcNow;
        }

        if (model.Name is not null && model.Name != request!.Name)
        {
            request.Name = model.Name;
            request.UpdatedAt = DateTime.UtcNow;
        }

        if (model.TotalAmount is not null && model.TotalAmount != request!.TotalAmount)
        {
            request.TotalAmount = model.TotalAmount.Value;
            request.UpdatedAt = DateTime.UtcNow;
        }

        if (model.ImageData is not null)
        {
            var currentUser = _userRepository
                .Query()
                .FirstOrDefault(user => user.Id == _httpContextAccessor.GetCurrentUserId());

            RuntimeValidator.Assert(currentUser is not null, StatusCode.Unauthorized);

            var avatarName = request!.Id.ToString();

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

            if (request.ImageDataUrl is not null)
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Files",
                    "Avatars",
                    request.ImageDataUrl
                );

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(model.ImageData), cancellationToken);

            request.ImageDataUrl = _urlSettings.WebApiUrl + "api/v1/requests/images/" + avatarName;
        }
    }

    public async Task DeleteRequestAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    )
    {
        var request = await _requestRepository
            .Query()
            .Include(request => request.OrganizationRequests)
            .FirstOrDefaultAsync(
                request => request.Id == requestId,
                cancellationToken
            );

        RuntimeValidator.Assert(request is not null, StatusCode.RequestNotFound);

        RuntimeValidator.Assert(request!.CreatedById == _httpContextAccessor.GetCurrentUserId(),
            StatusCode.YouAreNotCreatorOfRequest);

        RuntimeValidator.Assert(request.OrganizationRequests?.Any() is not true, StatusCode.RequestAlreadyHasDonations);

        _requestRepository.Delete(request);

        await _requestRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    )
    {
        var request = await _requestRepository
            .Query()
            .FirstOrDefaultAsync(
                request => request.Id == model.RequestId,
                cancellationToken
            );

        RuntimeValidator.Assert(request is not null, StatusCode.RequestNotFound);

        var currentUser = _userRepository
            .Query()
            .FirstOrDefault(user => user.Id == _httpContextAccessor.GetCurrentUserId());

        RuntimeValidator.Assert(currentUser?.OrganizationId is not null, StatusCode.Forbidden);

        var organizationRequest = _mapper.Map<OrganizationRequest>(model);

        organizationRequest.OrganizationId = currentUser!.OrganizationId!.Value;

        request!.OrganizationRequests = (request.OrganizationRequests ?? new List<OrganizationRequest>())
            .Append(organizationRequest)
            .ToList();

        await _requestRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<RequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = _userRepository
            .Query()
            .FirstOrDefault(user => user.Id == _httpContextAccessor.GetCurrentUserId());

        RuntimeValidator.Assert(user is not null, StatusCode.Unauthorized);

        IQueryable<Request> requests;

        if (user!.OrganizationId == null)
        {
            requests = _requestRepository
                .Query()
                .Include(request => request.OrganizationRequests)
                .Where(request => request.CreatedById == user.Id);
        }
        else
        {
            requests = _requestRepository
                .Query()
                .Include(request => request.OrganizationRequests)
                .Where(request => request.OrganizationRequests!.Any(organizationRequest =>
                    organizationRequest.OrganizationId == user.OrganizationId));
        }

        return _mapper.Map<IEnumerable<RequestView>>(await requests.ToListAsync(cancellationToken));
    }
}