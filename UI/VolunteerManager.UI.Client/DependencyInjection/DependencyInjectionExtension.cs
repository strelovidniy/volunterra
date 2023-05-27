using Blazored.LocalStorage;
using VolunteerManager.UI.Domain.DependencyInjection;
using VolunteerManager.UI.Shared.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace VolunteerManager.UI.Client.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection RegisterUiClientApplication(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .RegisterUiShared()
        .RegisterUiConnectorLayer(configuration)
        .RegisterMudBlazor()
        .AddBlazoredLocalStorage();

    private static IServiceCollection RegisterMudBlazor(
        this IServiceCollection services
    ) => services.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 5000;
        config.SnackbarConfiguration.HideTransitionDuration = 500;
        config.SnackbarConfiguration.ShowTransitionDuration = 500;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        config.SnackbarConfiguration.ClearAfterNavigation = false;
    });
}