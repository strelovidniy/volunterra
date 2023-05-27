using System.Net.Http.Headers;
using Blazored.LocalStorage;
using VolunteerManager.Models;
using VolunteerManager.UI.Domain.Settings.Abstraction;

namespace VolunteerManager.UI.Domain.Http.HttpClientFactory;

internal class HttpClientFactory : IHttpClientFactory
{
    private readonly IUrlSettings _urlSettings;
    private readonly ILocalStorageService _localStorageService;

    public HttpClientFactory(
        ILocalStorageService localStorageService,
        IUrlSettings urlSettings
    )
    {
        _localStorageService = localStorageService;
        _urlSettings = urlSettings;
    }

    public async Task<HttpClient> GetInstanceAsync(
        CancellationToken cancellationToken = default
    )
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri(_urlSettings.WebApiUrl);

        try
        {
            var token = await _localStorageService.GetItemAsync<AuthToken>("token", cancellationToken);

            if (token?.Token is not null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
            }
        }
        catch
        {
            // ignored
        }

        return client;
    }
}