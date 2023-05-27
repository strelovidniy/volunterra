using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VolunteerManager.Data.Entities;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Domain.Helpers;
using VolunteerManager.Domain.Services.Abstraction;

namespace VolunteerManager.Domain.Services.Realization;

internal class ValidationService : IValidationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Organization> _organizationRepository;
    private readonly IRepository<Request> _requestRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidationService(
        IRepository<User> userRepository,
        IHttpContextAccessor httpContextAccessor,
        IRepository<Organization> organizationRepository,
        IRepository<Request> requestRepository
    )
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _organizationRepository = organizationRepository;
        _requestRepository = requestRepository;
    }

    public Task<bool> IsUserExistsAsync(
        string email,
        CancellationToken cancellationToken = default
    ) => _userRepository
        .Query()
        .AnyAsync(
            user => user.Email == email,
            cancellationToken
        );

    public Task<bool> IsUserExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => _userRepository
        .Query()
        .AnyAsync(
            user => user.Id == id,
            cancellationToken
        );

    public bool IsUserIsCurrentUser(
        Guid id
    ) => _httpContextAccessor.GetCurrentUserId() == id;


    public Task<bool> IsUserWithVerificationCodeExistsAsync(
        Guid verificationCode,
        CancellationToken cancellationToken = default
    ) => _userRepository
        .Query()
        .AnyAsync(
            user => user.VerificationCode == verificationCode,
            cancellationToken
        );

    public Task<bool> IsInvitedUserExistAsync(
        Guid invitationToken,
        CancellationToken cancellationToken = default
    ) => _userRepository
        .Query()
        .AnyAsync(
            user => user.InvitationToken == invitationToken,
            cancellationToken
        );

    public Task<bool> IsCurrentUserPasswordCorrectAsync(
        string password,
        CancellationToken cancellationToken = default
    ) => _userRepository
        .Query()
        .AnyAsync(
            user => user.PasswordHash == PasswordHasher.GetHash(password)
                && user.Id == _httpContextAccessor.GetCurrentUserId(),
            cancellationToken
        );

    public async Task<bool> IsEmailUniqueAsync(
        string email,
        CancellationToken cancellationToken = default
    ) => !await _userRepository
        .Query()
        .AnyAsync(
            user => user.Email == email,
            cancellationToken
        );

    public async Task<bool> IsOrganizationNameUniqueAsync(
        string name,
        CancellationToken cancellationToken = default
    ) => !await _organizationRepository
        .Query()
        .AnyAsync(
            organization => organization.Name == name,
            cancellationToken
        );

    public Task<bool> IsOrganizationExistAsync(
        Guid organizationId,
        CancellationToken cancellationToken = default
    ) => _organizationRepository
        .Query()
        .AnyAsync(
            organization => organization.Id == organizationId,
            cancellationToken
        );

    public Task<bool> IsRequestExistAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    ) => _requestRepository
        .Query()
        .AnyAsync(
            request => request.Id == requestId,
            cancellationToken
        );
}