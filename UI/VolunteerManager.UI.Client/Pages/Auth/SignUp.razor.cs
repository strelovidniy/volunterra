using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages.Auth;

public partial class SignUp : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly CreateUserModel _model = new();
    private readonly CreateOrganizationModel _extendedModel = new();
    private int step = 1;

    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    private bool _isSuccessSubmit = true;

    private bool _processing;

    private MudForm _form = null!;
    private MudForm _extendedForm = null!;

    [Inject]
    private IUserService UserService { get; set; } = null!;

    [Inject]
    private IOrganizationService OrganizationService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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
        _form?.Dispose();
        _extendedForm?.Dispose();
        Snackbar.Dispose();
    }


    private void OnValidationError(ApiErrorResult? _)
    {
        _isSuccessSubmit = false;
    }

    private void OnError(Exception _)
    {
        _isSuccessSubmit = false;
    }

    protected override Task OnInitializedAsync()
    {
        HttpClient.OnValidationError += OnValidationError;
        HttpClient.OnError += OnError;

        return Task.CompletedTask;
    }

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

    private static string? ValidatePassword(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Password is required";
        }

        return Regex.IsMatch(
            value,
            @"[A-Z]"
        )
            ? Regex.IsMatch(
                value,
                @"[a-z]"
            )
                ? Regex.IsMatch(
                    value,
                    @"[0-9]"
                )
                    ? value.Length >= 8
                        ? value.Length <= 32
                            ? null
                            : "Password max length is 32 characters"
                        : "Password must be at least 8 characters long"
                    : "Password must have at least one digit"
                : "Password must have at least one lowercase letter"
            : "Password must have at least one uppercase letter";
    }

    private static string? ValidateEmailField(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Email is required";
        }

        return Regex.IsMatch(
            value,
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            RegexOptions.IgnoreCase
        )
            ? null
            : "Email format is not valid";
    }

    private string? ValidateConfirmPassword(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Password confirmation is required";
        }

        return _model.Password != value ? "Passwords do not match" : null;
    }

    private string? ValidateConfirmPasswordExtended(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Password confirmation is required";
        }

        return _extendedModel.Password != value ? "Passwords do not match" : null;
    }

    private static string? ValidateOrganizationName(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "Organization name is required";
        }

        return value.Length > 250 ? "Organization name too long" : null;
    }

    private static string? ValidateOrganizationDescription(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value.Length > 2000 ? "Organization description too long" : null;
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

            await UserService.CreateUserAsync(
                new CreateUserModel
                {
                    Password = _model.Password,
                    ConfirmPassword = _model.ConfirmPassword,
                    FirstName = _model.FirstName,
                    LastName = _model.LastName,
                    Email = _model.Email
                },
                _cts.Token
            );

            if (_isSuccessSubmit)
            {
                NavigationManager.NavigateTo("/auth/login");
            }
            else
            {
                _isSuccessSubmit = true;
            }

            _processing = false;
        }
        catch
        {
            _processing = false;
        }
    }

    private async Task OnSubmitExtendedAsync()
    {
        try
        {
            _processing = true;

            await _extendedForm.Validate();

            if (!_extendedForm.IsValid)
            {
                Snackbar.Add(_extendedForm.Errors.FirstOrDefault(), Severity.Error);

                _processing = false;

                return;
            }

            await OrganizationService.CreateOrganizationAsync(
                new CreateOrganizationModel
                {
                    Password = _extendedModel.Password,
                    ConfirmPassword = _extendedModel.ConfirmPassword,
                    FirstName = _extendedModel.FirstName,
                    LastName = _extendedModel.LastName,
                    Email = _extendedModel.Email,
                    OrganizationDescription = _extendedModel.OrganizationDescription,
                    OrganizationName = _extendedModel.OrganizationName
                },
                _cts.Token
            );

            if (_isSuccessSubmit)
            {
                NavigationManager.NavigateTo("/auth/login");
            }
            else
            {
                _isSuccessSubmit = true;
            }

            _processing = false;
        }
        catch
        {
            _processing = false;
        }
    }

    private void ReleaseUnmanagedResources()
    {
        HttpClient.OnValidationError -= OnValidationError;
        HttpClient.OnError -= OnError;
    }

    ~SignUp()
    {
        Dispose(false);
    }
}