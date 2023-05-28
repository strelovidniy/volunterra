using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages;

public partial class EventDetail : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private OrganizationRequestView? _organizationRequest;

    private bool _isPageLoading = true;

    private bool _processing;

    [SupplyParameterFromQuery]
    [Parameter]
    public Guid EventId { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; } = null!;

    [Inject]
    private IOrganizationRequestService OrganizationRequestService { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync()
    {
        _isPageLoading = true;

        _organizationRequest = await OrganizationRequestService.GetOrganizationRequestAsync(EventId, _cts.Token);

        if (_organizationRequest is null)
        {
            NavigationManager.NavigateTo("/");
        }

        _isPageLoading = false;
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
        Snackbar.Dispose();
    }

    ~EventDetail()
    {
        Dispose(false);
    }
}