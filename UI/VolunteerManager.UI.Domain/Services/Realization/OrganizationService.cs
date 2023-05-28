using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class OrganizationService : IOrganizationService
{
    private readonly IVolunteerManagerHttpClient _httpClient;

    public OrganizationService(
        IVolunteerManagerHttpClient httpClient
    ) => _httpClient = httpClient;

    public Task CreateOrganizationAsync(
        CreateOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/organizations/organization-sign-up",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task UpdateOrganizationAsync(
        UpdateOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PutAsync(
            "api/v1/organizations",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task DeleteOrganizationAsync(
        Guid organizationId,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .DeleteAsync(
            $"api/v1/organizations/{organizationId}",
            cancellationToken
        );

    public Task InviteUserToOrganizationAsync(
        InviteUserToOrganizationModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/organizations/invite",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task RemoveUserFromOrganizationAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .DeleteAsync(
            $"api/v1/organizations/users/{userId}",
            cancellationToken
        );

    public Task AcceptInviteAsync(
        Guid invitationToken,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .GetAsync(
            $"api/v1/organizations/accept-invite/{invitationToken}",
            cancellationToken
        );
}