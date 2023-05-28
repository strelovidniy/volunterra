using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using OData.QueryBuilder.Builders;
using VolunteerManager.Data.Entities;
using VolunteerManager.Models.Create;
using VolunteerManager.Models.Views;
using VolunteerManager.UI.Domain.Http.VolunteerManagerHttpClient;
using VolunteerManager.UI.Domain.Services.Abstraction;

namespace VolunteerManager.UI.Client.Pages;

public partial class CreateNewEvent : IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    private readonly CreateOrganizationRequestModel _model = new();

    private int _selectedIndex;

    private bool _processing;

    private bool _isPageLoading = true;

    private UserView? _currentUser;

    private MudForm _form = null!;

    private List<string> _selectedSkills = new();
    private List<string> _skills = new();

    [Inject]
    private IVolunteerManagerHttpClient HttpClient { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

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

        _currentUser = await AuthService.GetCurrentUserAsync(_cts.Token);

        var builder = new ODataQueryBuilder("api/odata")
            .For<Skill>("skills")
            .ByList();

        var response = await HttpClient.GetFromOdataAsync<SkillView>(builder, _cts.Token);

        _skills = response?.Value?.Select(x => x.Name).ToList() ?? _skills;

        _isPageLoading = false;
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

                _model.Skills = _selectedSkills;

                await OrganizationRequestService.CreateOrganizationRequestAsync(_model);

                _processing = false;

                return;
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

    ~CreateNewEvent()
    {
        Dispose(false);
    }
}