using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class RequestService : IRequestService
{
    private readonly IVolunteerManagerHttpClient _httpClient;

    public RequestService(
        IVolunteerManagerHttpClient httpClient
    ) => _httpClient = httpClient;

    public Task CreateRequestAsync(
        CreateRequestModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync(
            "api/v1/requests",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task UpdateRequestAsync(
        UpdateRequestModel model,
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
            "api/v1/requests/organization",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task<IEnumerable<RequestView>?> GetRequestsAsync(
        CancellationToken cancellationToken = default
    ) => _httpClient
        .GetAsync<IEnumerable<RequestView>>(
            "api/v1/requests",
            cancellationToken
        );
}