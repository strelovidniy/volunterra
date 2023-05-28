using Microsoft.AspNetCore.Components;
using MudBlazor;
using OData.QueryBuilder.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages;

public partial class Achievements : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly List<(string Src, string Title, string Description)> _achievements = new()
    {
        ("images/achievements/beginner.svg", "Beginner", "complete one event as a volunteer"),
        ("images/achievements/teamplayer.svg", "Team player", "complete 50 events as a volunteer"),
        ("images/achievements/know-it-all.svg", "Know-it-all", "complete 100 events as a volunteer"),
        ("images/achievements/eco-warrior.svg", "Eco-warrior", "to take part in an environmental event"),
        ("images/achievements/heart-on-time.svg", "Heart on Time",
            "fill your entire working day with volunteer activities"),
        ("images/achievements/time-management.svg", "Time management",
            "complete 50 volunteer events in 1 calendar year"),
        ("images/achievements/time-magi.svg", "Time Magi", "volunteer 100 hours"),
        ("images/achievements/melanka-podolyak.svg", "Melanka Podolyak", "volunteer 150 hours"),
        ("images/achievements/time-patron.svg", "Time Patron", "volunteer 500 hours")
    };

    private int _selectedIndex;

    private bool _processing;

    private bool _isPageLoading = true;

    private UserView? _currentUser;

    private List<OrganizationRequestView> _organizationRequests = new();

    private List<(string Src, string Title, string Description)> _filteredAchievements = new();

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

        ChangeIndex(0);

        _isPageLoading = false;
    }

    private void ChangeIndex(int index)
    {
        _selectedIndex = index;

        _filteredAchievements = index switch
        {
            0 => _achievements.Where(x => _currentUser?.Achievements.Any(ua => ua.Title == x.Title) is true).ToList(),
            _ => _achievements
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

        _cts.Cancel();
        _cts.Dispose();
        Snackbar.Dispose();
    }

    ~Achievements()
    {
        Dispose(false);
    }
}