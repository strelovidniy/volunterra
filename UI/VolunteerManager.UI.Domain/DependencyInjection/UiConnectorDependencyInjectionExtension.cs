using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VolunteerManager.UI.Domain.Http.HttpClientFactory;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;
using VolunteerManager.UI.Domain.Services.Realization;
using VolunteerManager.UI.Domain.Settings.Abstraction;
using VolunteerManager.UI.Domain.Settings.Realization;
using IHttpClientFactory = VolunteerManager.UI.Domain.Http.HttpClientFactory.IHttpClientFactory;

namespace VolunteerManager.UI.Domain.DependencyInjection;

public static class UiConnectorDependencyInjectionExtension
{
    public static IServiceCollection RegisterUiConnectorLayer(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddServices()
        .AddSettings(configuration);

    private static IServiceCollection AddServices(
        this IServiceCollection services
    ) => services
        .AddScoped<IHttpClientFactory, HttpClientFactory>()
        .AddScoped<IVolunteerManagerHttpClient, VolunteerManagerHttpClient>()
        .AddTransient<IUserService, UserService>()
        .AddTransient<IAuthService, AuthService>()
        .AddTransient<IOrganizationService, OrganizationService>()
        .AddTransient<IRequestService, RequestService>();

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var urlSettings = new UrlSettings();

        configuration
            .GetSection("UrlSettings")
            .Bind(urlSettings);

        services
            .AddTransient<IUrlSettings, UrlSettings>(_ => urlSettings);

        return services;
    }
}