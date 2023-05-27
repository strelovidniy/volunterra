using Blazored.LocalStorage;
using VolunteerManager.Models;
using VolunteerManager.UI.Domain.Services.Abstraction;
using VolunteerManager.UI.Shared.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace VolunteerManager.UI.Client.Pages.Auth;

public partial class Login : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly LoginModel _model = new();

    private readonly DialogOptions _dialogOptions = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.Small,
        FullWidth = true,
        DisableBackdropClick = true
    };

    private bool _processing;

    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    private MudForm _form = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void GoToSignUp() => NavigationManager.NavigateTo("auth/sign-up");

    private void TogglePasswordVisibility()
    {
        if (_passwordVisibility)
        {
            _passwordVisibility = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordVisibility = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }

    public async Task ResetPasswordAsync()
    {
        var dialog = await DialogService.ShowAsync<ResetPasswordDialog>("Reset Password", _dialogOptions);

        var result = await dialog.Result;

        if (!result.Canceled)
        {
            Snackbar.Add("Password reset link has been sent to your email", Severity.Success);
        }
    }

    private async Task OnSubmitAsync()
    {
        try
        {
            _processing = true;

            await _form.Validate();

            if (!_form.IsValid)
            {
                Snackbar.Add(_form.Errors.FirstOrDefault(), Severity.Error);

                _processing = false;

                return;
            }

            var res = await AuthService.LoginAsync(
                new LoginModel
                {
                    Password = _model.Password,
                    Email = _model.Email
                },
                _cts.Token
            );

            await LocalStorageService.SetItemAsync("token", res.Token);

            var returnUrl = await LocalStorageService.GetItemAsync<string>("returnUrl");

            Console.WriteLine(returnUrl);

            await LocalStorageService.RemoveItemAsync("returnUrl");

            NavigationManager.NavigateTo(
                !string.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/", true);

            _processing = false;
        }
        catch
        {
            await LocalStorageService.RemoveItemAsync("token");
            _processing = false;
        }
    }

    private void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
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
        _form.Dispose();
        Snackbar.Dispose();
    }

    ~Login()
    {
        Dispose(false);
    }
}