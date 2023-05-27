namespace VolunteerManager.UI.Domain.Http.HttpClientFactory;

public interface IHttpClientFactory
{
    public Task<HttpClient> GetInstanceAsync(
        CancellationToken cancellationToken = default
    );
}