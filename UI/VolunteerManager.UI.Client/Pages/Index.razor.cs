using Microsoft.AspNetCore.Components;
using MudBlazor;
using OData.QueryBuilder.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages;

public partial class Index : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private int _selectedIndex;

    private bool _processing;

    private bool _isPageLoading = true;

    private UserView? _currentUser;

    private List<OrganizationRequestView> _organizationRequests = new();

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync()
    {
        _isPageLoading = true;

        _currentUser = await AuthService.GetCurrentUserAsync(_cts.Token);

        var builder = new ODataQueryBuilder("api/odata")
            .For<OrganizationRequestReply>("organizationRequestsInvocations")
            .ByList();

        var response = await HttpClient.GetFromOdataAsync<OrganizationRequestView>(builder, _cts.Token);

        _organizationRequests = response?.Value ?? _organizationRequests;

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

    ~Index()
    {
        Dispose(false);
    }
}