using Microsoft.AspNetCore.Components;
using MudBlazor;
using OData.QueryBuilder.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.HomeAccountingHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages;

public partial class Index : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly List<string> _placeholders = new()
    {
        "images/placeholders/photo_2023-05-28_11-46-15.jpg",
        "images/placeholders/photo_2023-05-28_11-46-17.jpg",
        "images/placeholders/photo_2023-05-28_11-46-19.jpg",
        "images/placeholders/photo_2023-05-28_11-46-21.jpg",
        "images/placeholders/photo_2023-05-28_11-46-23.jpg",
        "images/placeholders/photo_2023-05-28_11-46-25.jpg",
        "images/placeholders/photo_2023-05-28_11-46-26.jpg",
        "images/placeholders/photo_2023-05-28_11-46-29.jpg",
        "images/placeholders/photo_2023-05-28_11-46-31.jpg"
    };

    private int _selectedIndex;

    private OrganizationRequestCategory? _categoryFilter;

    private bool _processing;

    private string? _locationFilter;

    private bool _isPageLoading = true;

    private UserView? _currentUser;

    private List<OrganizationRequestView> _organizationRequests = new();
    private List<OrganizationRequestReplyView> _organizationRequestReplies = new();

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

    private string GetImageUrl()
    {
        var rnd = new Random();

        return _placeholders[rnd.Next(0, _placeholders.Count)];
    }

    protected override async Task OnInitializedAsync()
    {
        _isPageLoading = true;

        _currentUser = await AuthService.GetCurrentUserAsync(_cts.Token);

        await ReloadDataAsync();

        _isPageLoading = false;
    }

    private async Task ReloadDataAsync()
    {
        _processing = true;

        //if (_currentUser?.Organization is null)
        //{
        var builder = new ODataQueryBuilder("api/odata")
            .For<OrganizationRequest>("organizationRequests")
            .ByList();

        if (_categoryFilter is not null)
        {
            builder = builder.Filter(x => x.OrganizationRequestCategory == _categoryFilter);
        }

        if (_locationFilter is not null)
        {
            builder = builder.Filter(x => x.Location == _locationFilter);
        }

        if (_currentUser?.Organization is not null)
        {
            builder = builder.Filter(x => x.OrganizationId == _currentUser.Organization.Id);
        }

        var response = await HttpClient.GetFromOdataAsync<OrganizationRequestView>(builder, _cts.Token);

        _organizationRequests = response?.Value ?? _organizationRequests;
        //}
        //else
        //{
        //    var builder = new ODataQueryBuilder("api/odata")
        //        .For<OrganizationRequestReply>("organizationRequestsInvocations")
        //        .ByList();

        //    if (_currentUser?.Organization is not null)
        //    {
        //        builder = builder.Filter(x => x.OrganizationRequest!.OrganizationId == _currentUser.Organization.Id);
        //    }

        //    var response = await HttpClient.GetFromOdataAsync<OrganizationRequestReplyView>(builder, _cts.Token);

        //    _organizationRequestReplies = response?.Value ?? _organizationRequestReplies;
        //}

        _processing = false;
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