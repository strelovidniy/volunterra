using VolunteerManager.Models.Create;
using VolunteerManager.Models.Update;
using VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

public class OrganizationRequestReplyService : IOrganizationRequestReplyService
{
    private readonly IVolunteerManagerHttpClient _httpClient;

    public OrganizationRequestReplyService(
        IVolunteerManagerHttpClient httpClient
    ) => _httpClient = httpClient;

    public Task CreateRequestReplyAsync(
        CreateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient
            .PostAsync(
                "api/v1/requestReplies",
                _httpClient.CreateJsonContent(model),
                cancellationToken
            );

    public Task UpdateRequestReplyAsync(
        UpdateOrganizationRequestReplyModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PutAsync(
            "api/v1/requestReplies",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task DeleteRequestReplyAsync(Guid requestId, CancellationToken cancellationToken = default) => _httpClient
        .DeleteAsync(
            $"api/v1/requestReplies/{requestId}",
            cancellationToken
        );
}