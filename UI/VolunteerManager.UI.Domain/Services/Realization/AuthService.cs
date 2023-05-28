using VolunteerManager.Models;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Domain.Services.Realization;

internal class AuthService : IAuthService
{
    private readonly IVolunteerManagerHttpClient _httpClient;

    public AuthService(
        IVolunteerManagerHttpClient httpClient
    ) => _httpClient = httpClient;

    public Task<LoginView?> LoginAsync(
        LoginModel model,
        CancellationToken cancellationToken = default
    ) => _httpClient
        .PostAsync<LoginView>(
            "api/v1/auth/login",
            _httpClient.CreateJsonContent(model),
            cancellationToken
        );

    public Task<UserView?> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    ) => _httpClient
        .GetAsync<UserView>(
            "api/v1/auth/me",
            cancellationToken
        );
}