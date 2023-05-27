using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;

namespace VolunteerManager.UI.Client.Pages;

public partial class Index : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly ChartOptions _chartOptions = new();

    private string[] _xAxisLabels = { };

    private List<ChartSeries> _series = new()
    {
        new ChartSeries
        {
            Name = "Incoming",
            Data = new double[] { }
        },
        new ChartSeries
        {
            Name = "Spending",
            Data = new double[] { }
        }
    };

    private int _selectedIndex = -1;

    private bool _processing;

    private bool _isPageLoading = true;

    //private List<SpendingView> _spendings = new();
    //private List<IncomingView> _incomings = new();

    private int _spendingsTotalCount;
    private int _incomingsTotalCount;

    private decimal _averageSpending;
    private decimal _averageIncoming;

    private DateRange _dateRange = new(
        DateTime.Now.AddDays(-7).Date,
        DateTime.Now.Date.AddDays(1).AddMilliseconds(-1)
    );

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    //[Inject]
    //private ISpendingService SpendingService { get; set; } = null!;

    //[Inject]
    //private IIncomingService IncomingService { get; set; } = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync()
    {
        _isPageLoading = true;

        //await LoadSpendingsAndIncomingsAsync(_cts.Token);

        _isPageLoading = false;
    }

    //private async Task LoadSpendingsAndIncomingsAsync(
    //    CancellationToken cancellationToken = default
    //)
    //{
    //    _processing = true;

    //    _averageIncoming = await IncomingService.GetAverageIncomingAsync(cancellationToken);
    //    _averageSpending = await SpendingService.GetAverageSpendingAsync(cancellationToken);

    //    var incomingsBuilder = new ODataQueryBuilder("api/odata")
    //        .For<Data.Entities.Incoming>("incomings")
    //        .ByList();

    //    if (_dateRange.Start.HasValue)
    //    {
    //        incomingsBuilder = incomingsBuilder
    //            .Filter(incoming => incoming.CreatedAt >= _dateRange.Start.Value);
    //    }

    //    if (_dateRange.End.HasValue)
    //    {
    //        incomingsBuilder = incomingsBuilder
    //            .Filter(incoming => incoming.CreatedAt <= _dateRange.End.Value);
    //    }

    //    incomingsBuilder = incomingsBuilder
    //        .OrderByDescending(incoming => incoming.CreatedAt);

    //    var incomingsResponse = await HttpClient.GetFromOdataAsync<IncomingView>(incomingsBuilder, cancellationToken);

    //    _incomings = incomingsResponse?.Value ?? _incomings;
    //    _incomingsTotalCount = incomingsResponse?.Count ?? _incomingsTotalCount;

    //    var spendingsBuilder = new ODataQueryBuilder("api/odata")
    //        .For<Data.Entities.Spending>("spendings")
    //        .ByList();

    //    if (_dateRange.Start.HasValue)
    //    {
    //        spendingsBuilder = spendingsBuilder
    //            .Filter(spending => spending.CreatedAt >= _dateRange.Start.Value);
    //    }

    //    if (_dateRange.End.HasValue)
    //    {
    //        spendingsBuilder = spendingsBuilder
    //            .Filter(spending => spending.CreatedAt <= _dateRange.End.Value);
    //    }

    //    spendingsBuilder = spendingsBuilder
    //        .OrderByDescending(spending => spending.CreatedAt);

    //    var spendingResponse = await HttpClient.GetFromOdataAsync<SpendingView>(spendingsBuilder, cancellationToken);

    //    _spendings = spendingResponse?.Value ?? _spendings;
    //    _spendingsTotalCount = spendingResponse?.Count ?? _incomingsTotalCount;

    //    var series = new List<ChartSeries>
    //    {
    //        new()
    //        {
    //            Name = "Spending",
    //            Data = Array.Empty<double>()
    //        },
    //        new()
    //        {
    //            Name = "Incoming",
    //            Data = Array.Empty<double>()
    //        }
    //    };

    //    var xAxisLabels = new List<string>();

    //    var period = _dateRange.End - _dateRange.Start ?? TimeSpan.Zero;

    //    if (_dateRange.Start.HasValue)
    //    {
    //        for (var i = 0; i < period.Days; i++)
    //        {
    //            var date = _dateRange.Start.Value.AddDays(i).Date;

    //            if (date > DateTime.Today)
    //            {
    //                break;
    //            }

    //            var spending = Convert.ToDouble(
    //                _spendings
    //                    .Where(spending => spending.SpendingDate.Date == date)
    //                    .Sum(spending => spending.Amount)
    //            );

    //            var incoming = Convert.ToDouble(
    //                _incomings
    //                    .Where(incoming => incoming.IncomingDate.Date == date)
    //                    .Sum(incoming => incoming.Amount)
    //            );

    //            series[0].Data = series[0].Data.Append(spending).ToArray();
    //            series[1].Data = series[1].Data.Append(incoming).ToArray();

    //            xAxisLabels.Add(date.ToString("dd.MM"));
    //        }
    //    }

    //    _series = series;
    //    _xAxisLabels = xAxisLabels.ToArray();
    //    _chartOptions.MaxNumYAxisTicks = 15;
    //    _chartOptions.YAxisFormat = "0 UAH";

    //    _processing = false;

    //    _isPageLoading = false;

    //    await InvokeAsync(StateHasChanged);

    //    await JsRuntime.InvokeVoidAsync("fixChart", cancellationToken);
    //}

    //private string GetListItemStyle(SpendingView spending)
    //{
    //    var spendingRatio = spending.Amount / _averageSpending;

    //    return "border-radius: 5px; margin-top: 5px; "
    //        + spendingRatio switch
    //        {
    //            <= 0.5M => "background-color: var(--mud-palette-success-hover); color: var(--mud-palette-success);",
    //            <= 1.0M => "background-color: var(--mud-palette-info-hover); color: var(--mud-palette-info);",
    //            <= 1.5M => "background-color: var(--mud-palette-warning-hover); color: var(--mud-palette-warning);",
    //            _ => "background-color: var(--mud-palette-error-hover); color: var(--mud-palette-error);"
    //        };
    //}

    //private string GetListItemStyle(IncomingView incoming)
    //{
    //    var incomingRatio = incoming.Amount / _averageIncoming;

    //    return "border-radius: 5px; margin-top: 5px; "
    //        + incomingRatio switch
    //        {
    //            <= 0.5M => "background-color: var(--mud-palette-error-hover); color: var(--mud-palette-error);",
    //            <= 1.0M => "background-color: var(--mud-palette-warning-hover); color: var(--mud-palette-warning);",
    //            <= 1.5M => "background-color: var(--mud-palette-info-hover); color: var(--mud-palette-info);",
    //            _ => "background-color: var(--mud-palette-success-hover); color: var(--mud-palette-success);"
    //        };
    //}

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