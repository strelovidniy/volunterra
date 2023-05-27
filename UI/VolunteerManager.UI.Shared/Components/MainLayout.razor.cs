using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using VolunteerManager.Models;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Shared.Components;

public partial class MainLayout : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private UserView? _currentUser;

    private bool _drawerOpen = true;
    private bool _settingsExpanded = true;
    private bool _adminExpanded = true;

    private string _theme = null!;
    private bool _isDarkMode;
    private MudThemeProvider _mudThemeProvider = null!;

    private bool _inited;

    private char? _firstLetterOfName;

    private string? _fullName;

    private IEnumerable<RequestView> _requests = new List<RequestView>();

    private bool IsAuth => NavManager.Uri.Contains("/auth/");

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavManager { get; set; } = null!;

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; } = null!;

    [Inject]
    private IRequestService RequestService { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Init()
    {
        HttpClient.OnForbidden += OnForbidden;
        HttpClient.OnUnauthorized += OnUnauthorized;
        HttpClient.OnInternalServerError += OnInternalServerError;
        HttpClient.OnBadRequest += OnBadRequest;
        HttpClient.OnNotFound += OnNotFound;
        HttpClient.OnValidationError += OnValidationError;
        HttpClient.OnError += OnError;
    }

    private void OnForbidden(HttpResponseMessage response)
    {
        //NavManager.NavigateTo($"auth/login?returnUrl={Uri.EscapeDataString(NavManager.Uri)}");
    }

    private void OnUnauthorized(HttpResponseMessage response)
    {
        LogoutAsync().AndForget();
    }

    private void OnBadRequest(HttpResponseMessage response)
    {
        //NavManager.NavigateTo($"auth/login?returnUrl={Uri.EscapeDataString(NavManager.Uri)}");
    }

    private void OnNotFound(HttpResponseMessage response)
    {
        //NavManager.NavigateTo($"auth/login?returnUrl={Uri.EscapeDataString(NavManager.Uri)}");
    }

    private void OnValidationError(ApiErrorResult? error)
    {
        Snackbar.Add(error?.Message, Severity.Error);
    }

    private void OnInternalServerError(ApiErrorResult? error)
    {
        Snackbar.Add(error?.Message, Severity.Error);
    }

    private void OnError(Exception ex)
    {
        Snackbar.Add(ex.Message, Severity.Error);
    }

    protected override async Task OnInitializedAsync()
    {
        Init();

        if (IsAuth)
        {
            _inited = true;

            return;
        }

        _requests = await RequestService.GetRequestsAsync(_cts.Token) ?? new List<RequestView>();

        _currentUser = await AuthService.GetCurrentUserAsync();

        if (_currentUser?.FirstName.Length > 0)
        {
            _firstLetterOfName = _currentUser.FirstName[0];
        }

        _fullName = $@"{_currentUser?.FirstName} {_currentUser?.LastName}";

        GetPermissions();

        _inited = true;
    }

    private void GetPermissions()
    {
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _theme = await LocalStorageService.GetItemAsync<string>("theme") ?? "System Default";

            await OnThemeChangedAsync(_theme);
        }
    }

    private async Task OnThemeChangedAsync(string theme)
    {
        _theme = theme;

        await LocalStorageService.SetItemAsStringAsync("theme", _theme);

        switch (_theme)
        {
            case "System Default":
                _isDarkMode = await _mudThemeProvider.GetSystemPreference();
                await _mudThemeProvider.WatchSystemPreference(OnSystemPreferenceChangedAsync);
                await InvokeAsync(StateHasChanged);

                break;
            case "Dark":
                _isDarkMode = true;
                await InvokeAsync(StateHasChanged);
                await _mudThemeProvider.WatchSystemPreference(_ => Task.CompletedTask);
                await InvokeAsync(StateHasChanged);

                break;
            case "Light":
                _isDarkMode = false;
                await InvokeAsync(StateHasChanged);
                await _mudThemeProvider.WatchSystemPreference(_ => Task.CompletedTask);
                await InvokeAsync(StateHasChanged);

                break;
        }
    }

    private Task OnSystemPreferenceChangedAsync(bool newValue)
    {
        _isDarkMode = newValue;

        return InvokeAsync(StateHasChanged);
    }


    private async Task LogoutAsync()
    {
        await LocalStorageService.RemoveItemAsync("token");
        NavManager.NavigateTo("auth/login");
    }

    private void ReleaseUnmanagedResources()
    {
        HttpClient.OnForbidden -= OnForbidden;
        HttpClient.OnUnauthorized -= OnUnauthorized;
        HttpClient.OnInternalServerError -= OnInternalServerError;
        HttpClient.OnBadRequest -= OnBadRequest;
        HttpClient.OnNotFound -= OnNotFound;
        HttpClient.OnValidationError -= OnValidationError;
        HttpClient.OnError -= OnError;
    }

    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();

        if (!disposing)
        {
            return;
        }

        _cts.Cancel();
        _cts.Dispose();
        Snackbar.Dispose();
        _mudThemeProvider.Dispose();
    }

    ~MainLayout()
    {
        Dispose(false);
    }
}