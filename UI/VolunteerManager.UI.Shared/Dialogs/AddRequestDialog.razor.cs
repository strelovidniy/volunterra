using Microsoft.AspNetCore.Components;
using MudBlazor;
using VolunteerManager.Models;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Shared.Dialogs;

public partial class AddRequestDialog : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly CreateOrganizationRequestModel _model = new();

    private bool _isDialogLoading = true;

    private bool _isSuccessSubmit = true;

    private bool _processing;

    private MudForm _form = null!;

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public UserView CurrentUser { get; set; } = null!;

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private IOrganizationRequestService RequestService { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void OnError(Exception _)
    {
        _isSuccessSubmit = false;
    }

    private void OnValidationError(ApiErrorResult? _)
    {
        _isSuccessSubmit = false;
    }

    protected override Task OnInitializedAsync()
    {
        HttpClient.OnError += OnError;
        HttpClient.OnValidationError += OnValidationError;

        _isDialogLoading = false;

        return Task.CompletedTask;
    }

    private static string? ValidateAmountField(decimal value) => value <= 0 ? "Amount must be greater than 0" : null;

    private static string? ValidateDescriptionField(string value) =>
        !string.IsNullOrEmpty(value) && value.Length > 2000
            ? "Description must be less than 2000 characters"
            : null;

    private static string? ValidateNameField(string value) =>
        !string.IsNullOrEmpty(value) && value.Length > 250
            ? "Description must be less than 250 characters"
            : null;

    private async Task OnSubmitAsync()
    {
        _processing = true;

        await _form.Validate();

        if (!_form.IsValid)
        {
            _processing = false;

            Snackbar.Add(_form.Errors.FirstOrDefault(), Severity.Error);

            return;
        }

        await RequestService.CreateRequestAsync(_model, _cts.Token);

        if (_isSuccessSubmit)
        {
            MudDialog.Close();

            return;
        }

        _processing = false;

        _isSuccessSubmit = true;
    }

    private void ReleaseUnmanagedResources()
    {
        HttpClient.OnError -= OnError;
        HttpClient.OnValidationError -= OnValidationError;
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
        MudDialog.Dispose();
        Snackbar.Dispose();
    }

    ~AddRequestDialog()
    {
        Dispose(false);
    }
}