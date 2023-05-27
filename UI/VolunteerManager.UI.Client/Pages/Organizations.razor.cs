using Microsoft.AspNetCore.Components;
using MudBlazor;
using OData.QueryBuilder.Builders;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages;

public partial class Organizations : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly DialogOptions _dialogOptions = new()
    {
        CloseButton = true,
        MaxWidth = MaxWidth.Medium,
        FullWidth = true,
        DisableBackdropClick = true
    };

    private bool _isPageLoading = true;

    private MudTable<OrganizationView> _table = null!;

    private bool _isLoading;

    private List<OrganizationView> _deposits = new();

    private string _searchString = string.Empty;

    private UserView _currentUser = null!;

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private IOrganizationService OrganizationService { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync()
    {
        _currentUser = await AuthService.GetCurrentUserAsync(_cts.Token);

        _isPageLoading = false;
    }

    private async Task<TableData<OrganizationView>> ServerReloadAsync(TableState state)
    {
        _isLoading = true;

        var builder = new ODataQueryBuilder("api/odata")
            .For<OrganizationView>("organizations")
            .ByList()
            .Top(state.PageSize)
            .Skip(state.PageSize * state.Page);

        builder = state.SortDirection switch
        {
            SortDirection.None => builder,
            SortDirection.Ascending => state.SortLabel switch
            {
                "Description" => builder.OrderBy(o => o.Description),
                "Name" => builder.OrderBy(o => o.Name),
                "CreatedAt" => builder.OrderBy(o => o.CreatedAt),
                _ => builder
            },
            SortDirection.Descending => state.SortLabel switch
            {
                "Description" => builder.OrderByDescending(o => o.Description),
                "Name" => builder.OrderByDescending(o => o.Name),
                "CreatedAt" => builder.OrderByDescending(o => o.CreatedAt),
                _ => builder
            },
            _ => builder
        };

        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            builder = builder.Filter(
                (role, function) => function.Contains(role.Description, _searchString)
            );
        }

        var oDataResult = await HttpClient.GetFromOdataAsync(builder);

        _deposits = oDataResult?.Value ?? _deposits;

        _isLoading = false;

        return new TableData<OrganizationView>
        {
            Items = _deposits,
            TotalItems = oDataResult?.Count ?? 0
        };
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

        _cts.Dispose();
        Snackbar.Dispose();
    }

    ~Organizations()
    {
        Dispose(false);
    }
}