using VolunteerManager.Models;
using VolunteerManager.UI.Domain.Models;
using OData.QueryBuilder.Conventions.AddressingEntities.Query;

namespace VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;

public interface IVolunteerManagerHttpClient
{
    public event Action<HttpResponseMessage> OnUnauthorized;

    public event Action<HttpResponseMessage> OnForbidden;

    public event Action<HttpResponseMessage> OnNotFound;

    public event Action<ApiErrorResult?> OnInternalServerError;

    public event Action<HttpResponseMessage> OnBadRequest;

    public event Action<ApiErrorResult?> OnValidationError;

    public event Action<Exception> OnError;

    public Task<ODataResponse<T>?> GetFromOdataAsync<T>(
        IODataQueryCollection<T> query,
        CancellationToken cancellationToken = default
    );

    public Task<ODataResponse<T>?> GetFromOdataAsync<T>(
        IODataQuery query,
        CancellationToken cancellationToken = default
    );

    public Task GetAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task<T?> GetAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task PutAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task PostAsync(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    );

    public Task<T?> PostAsync<T>(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    );

    public Task PutAsync(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    );

    public Task<T?> PutAsync<T>(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    );

    public Task<T?> PutAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task DeleteAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task<T?> DeleteAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task PatchAsync(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    );

    public Task<T?> PatchAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task PatchAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    );

    public Task<T?> PatchAsync<T>(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    );

    public HttpContent CreateJsonContent<T>(
        T? data
    );
}