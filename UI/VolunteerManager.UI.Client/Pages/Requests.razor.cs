using Microsoft.AspNetCore.Components;
using MudBlazor;
using OData.QueryBuilder.Builders;
using VolunteerManager.Models;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;
using VolunteerManager.UI.Shared.Dialogs;

namespace VolunteerManager.UI.Client.Pages;

public partial class Requests : IDisposable
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

    private MudTable<OrganizationRequestView> _table = null!;

    private bool _isLoading;

    private List<OrganizationRequestView> _requests = new();

    private string _searchString = string.Empty;

    private UserView _currentUser = null!;

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private IOrganizationRequestService RequestService { get; set; } = null!;

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

    private async Task AddRequestsDialogAsync()
    {
        var parameters = new DialogParameters
        {
            { nameof(AddRequestDialog.CurrentUser), _currentUser }
        };

        var dialog = await DialogService.ShowAsync<AddRequestDialog>("Add Request", parameters, _dialogOptions);

        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await _table.ReloadServerData();
            Snackbar.Add("Request created successfully.", Severity.Success);
        }
    }

    private async Task UpdateRequestDialogAsync(OrganizationRequestView request)
    {
        var parameters = new DialogParameters
        {
            { nameof(UpdateRequestDialog.SelectedRequest), request }
        };

        var dialog = await DialogService.ShowAsync<UpdateRequestDialog>(
            "Update Request",
            parameters,
            new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.ExtraExtraLarge,
                FullWidth = true,
                DisableBackdropClick = true
            });

        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await _table.ReloadServerData();
            Snackbar.Add("Request updated successfully.", Severity.Success);
        }
    }

    private async Task DeleteRequestAsync(
        OrganizationRequestView request,
        CancellationToken cancellationToken = default
    )
    {
        var parameters = new DialogParameters
        {
            ["Text"] = "Are you sure you want to delete this Request?"
        };

        var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirmation", parameters, _dialogOptions);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var isSuccess = true;

            void OnError(Exception _)
            {
                isSuccess = false;
            }

            void OnValidationError(ApiErrorResult? _)
            {
                isSuccess = false;
            }

            HttpClient.OnError += OnError;
            HttpClient.OnValidationError += OnValidationError;

            await RequestService.DeleteRequestAsync(request.Id, cancellationToken);

            if (!isSuccess)
            {
                HttpClient.OnError -= OnError;
                HttpClient.OnValidationError -= OnValidationError;

                return;
            }

            await _table.ReloadServerData();
            Snackbar.Add("Request deleted successfully.", Severity.Success);

            HttpClient.OnError -= OnError;
            HttpClient.OnValidationError -= OnValidationError;
        }
    }

    private async Task<TableData<OrganizationRequestView>> ServerReloadAsync(TableState state)
    {
        _isLoading = true;

        var builder = new ODataQueryBuilder("api/odata")
            .For<OrganizationRequestView>("requests")
            .ByList()
            .Top(state.PageSize)
            .Skip(state.PageSize * state.Page);

        builder = state.SortDirection switch
        {
            SortDirection.None => builder,
            SortDirection.Ascending => state.SortLabel switch
            {
                "Description" => builder.OrderBy(o => o.Description),
                "RequestDate" => builder.OrderBy(o => o.RequestDate),
                "Amount" => builder.OrderBy(o => o.TotalAmount),
                "RequestUpdatedAt" => builder.OrderBy(o => o.RequestUpdatedAt),
                _ => builder
            },
            SortDirection.Descending => state.SortLabel switch
            {
                "Description" => builder.OrderByDescending(o => o.Description),
                "RequestDate" => builder.OrderByDescending(o => o.RequestDate),
                "Amount" => builder.OrderByDescending(o => o.TotalAmount),
                "RequestUpdatedAt" => builder.OrderByDescending(o => o.RequestUpdatedAt),
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

        _requests = oDataResult?.Value ?? _requests;

        _isLoading = false;

        return new TableData<OrganizationRequestView>
        {
            Items = _requests,
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

    ~Requests()
    {
        Dispose(false);
    }
}