using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class OrganizationRequestService : IOrganizationRequestService
{
    private readonly IVolunteerManagerHttpClient _httpClient;

    public OrganizationRequestService(
        IVolunteerManagerHttpClient httpClient
    ) => _httpClient = httpClient;

    public Task CreateRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/requests",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );


    public Task UpdateRequestAsync(
        UpdateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PutAsync(
            "api/v1/requests",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task DeleteRequestAsync(
        Guid requestId,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .DeleteAsync(
            $"api/v1/requests/{requestId}",
            cancellationToken
        );

    public Task CreateOrganizationRequestAsync(
        CreateOrganizationRequestModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/organizationRequests",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task<IEnumerable<OrganizationRequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    ) => _httpClient
        .GetAsync<IEnumerable<OrganizationRequestView>>(
            "api/v1/requests",
            cancellationToken
        );
}