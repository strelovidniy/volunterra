namespace VolunteerManager.Domain.Services.Abstraction;

public interface IValidationService
{
    public Task<bool> IsUserExistsAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsUserExistsAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    public bool IsUserIsCurrentUser(
        Guid id
    );

    public Task<bool> IsUserWithVerificationCodeExistsAsync(
        Guid verificationCode,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsInvitedUserExistAsync(
        Guid invitationToken,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsCurrentUserPasswordCorrectAsync(
        string password,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsEmailUniqueAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsOrganizationNameUniqueAsync(
        string name,
        CancellationToken cancellationToken = default
    );

    public Task<bool> IsOrganizationExistAsync(
        Guid organizationId,
        CancellationToken cancellationToken = default
    );
}