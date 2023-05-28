using Blazor.Cropper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using VolunteerManager.Models;
using VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Shared.Dialogs;

public partial class AvatarCropDialog : IDisposable
{
    private readonly CancellationTokenSource _cts = new();
    private Cropper _cropper = null!;
    private bool _processing;
    private bool _isSuccessSubmit = true;

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public IBrowserFile? RawImage { get; set; }

    [Inject]
    private IUserService UserService { get; set; } = null!;

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected override Task OnInitializedAsync()
    {
        HttpClient.OnValidationError += OnValidationError;
        HttpClient.OnError += OnError;

        return Task.CompletedTask;
    }

    private void OnValidationError(ApiErrorResult? _)
    {
        _isSuccessSubmit = false;
    }

    private void OnError(Exception _)
    {
        _isSuccessSubmit = false;
    }

    private void UploadFiles(IBrowserFile file)
    {
        RawImage = file;
    }

    private async Task SubmitAsync(
        CancellationToken cancellationToken = default
    )
    {
        _processing = true;
        await Task.Delay(300, cancellationToken);

        await InvokeAsync(StateHasChanged);

        var result = await _cropper.GetCropedResult();

        var bytes = await result.GetDataAsync();

        await UserService.ChangeAvatarAsync(bytes, cancellationToken);

        if (_isSuccessSubmit)
        {
            MudDialog.Close();

            return;
        }

        _isSuccessSubmit = true;

        _processing = false;
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
        MudDialog.Dispose();
    }

    ~AvatarCropDialog()
    {
        Dispose(false);
    }
}