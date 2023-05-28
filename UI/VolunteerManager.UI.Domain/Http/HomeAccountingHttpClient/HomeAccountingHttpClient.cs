using System.Net;
using System.Text;
using Newtonsoft.Json;
using OData.QueryBuilder.Conventions.AddressingEntities.Query;
using VolunteerManager.Models;
using VolunteerManager.UI.Domain.Models;
using IHttpClientFactory = VolunteerManager.UI.Domain.Http.HttpClientFactory.IHttpClientFactory;

namespace VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;

internal class VolunteerManagerHttpClient : IVolunteerManagerHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public VolunteerManagerHttpClient(
        IHttpClientFactory httpClientFactory
    ) =>
        _httpClientFactory = httpClientFactory;

    public async Task GetAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .GetAsync(
                    requestUri,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public async Task<T?> GetAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .GetAsync(
                    requestUri,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public async Task PostAsync(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .PostAsync(
                    requestUri,
                    content,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public async Task<T?> PostAsync<T>(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .PostAsync(
                    requestUri,
                    content,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public async Task PutAsync(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .PutAsync(
                    requestUri,
                    content,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public async Task<T?> PutAsync<T>(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .PutAsync(
                    requestUri,
                    content,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public async Task<T?> PutAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .PutAsync(
                    requestUri,
                    null,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public async Task DeleteAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .DeleteAsync(
                    requestUri,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public async Task<T?> DeleteAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .DeleteAsync(
                    requestUri,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public async Task PatchAsync(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .PatchAsync(
                    requestUri,
                    content,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public async Task PatchAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .PatchAsync(
                    requestUri,
                    null,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public async Task<T?> PatchAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .PatchAsync(
                    requestUri,
                    null,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public async Task<T?> PatchAsync<T>(
        string requestUri,
        HttpContent? content = default,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .PatchAsync(
                    requestUri,
                    content,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<T>(stringResult);

            return result;
        }
        catch
        {
            return default;
        }
    }

    public event Action<HttpResponseMessage> OnUnauthorized = delegate { };

    public event Action<HttpResponseMessage> OnForbidden = delegate { };

    public event Action<HttpResponseMessage> OnNotFound = delegate { };

    public event Action<HttpResponseMessage> OnBadRequest = delegate { };

    public event Action<Exception> OnError = delegate { };

    public async Task PutAsync(
        string requestUri,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            var response = await httpClient
                .PutAsync(
                    requestUri,
                    null,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);
        }
    }

    public HttpContent CreateJsonContent<T>(
        T? data
    ) => new StringContent(
        JsonConvert.SerializeObject(data),
        Encoding.UTF8,
        "application/json"
    );

    public async Task<ODataResponse<T>?> GetFromOdataAsync<T>(
        IODataQueryCollection<T> query,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        var requestUri = query.ToUri(UriKind.Relative).ToString();

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .GetAsync(
                    requestUri,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<ODataResponse<T>>(stringResult) ?? default;
        }
        catch
        {
            return default;
        }
    }

    public async Task<ODataResponse<T>?> GetFromOdataAsync<T>(
        IODataQuery query,
        CancellationToken cancellationToken = default
    )
    {
        HttpResponseMessage response;

        var requestUri = query.ToUri(UriKind.Relative).ToString();

        try
        {
            var httpClient = await _httpClientFactory
                .GetInstanceAsync(
                    cancellationToken
                );

            response = await httpClient
                .GetAsync(
                    requestUri,
                    cancellationToken
                );

            await ObserveHttpResponseAsync(response);
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);

            return default;
        }

        try
        {
            response.EnsureSuccessStatusCode();

            var stringResult = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<ODataResponse<T>>(stringResult) ?? default;
        }
        catch
        {
            return default;
        }
    }

    public event Action<ApiErrorResult?> OnInternalServerError = delegate { };

    public event Action<ApiErrorResult?> OnValidationError = delegate { };

    private async Task ObserveHttpResponseAsync(
        HttpResponseMessage response
    )
    {
        if (response.StatusCode is HttpStatusCode.InternalServerError)
        {
            var stringResponseContent = await response
                .Content
                .ReadAsStringAsync();

            var apiErrorResult = default(ApiErrorResult);

            try
            {
                apiErrorResult = JsonConvert.DeserializeObject<ApiErrorResult>(stringResponseContent);
            }
            catch
            {
                OnError.Invoke(
                    new Exception(
                        stringResponseContent
                    )
                );
            }

            OnInternalServerError.Invoke(
                apiErrorResult
            );
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            try
            {
                var stringResponseContent = await response
                    .Content
                    .ReadAsStringAsync();

                var apiErrorResult = JsonConvert.DeserializeObject<ApiErrorResult>(stringResponseContent);

                OnValidationError.Invoke(
                    apiErrorResult
                );
            }
            catch
            {
                OnBadRequest.Invoke(response);
            }
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
                OnUnauthorized.Invoke(response);

                break;
            case HttpStatusCode.Forbidden:
                OnForbidden.Invoke(response);

                break;
            case HttpStatusCode.NotFound:
                OnNotFound.Invoke(response);

                break;
        }
    }
}