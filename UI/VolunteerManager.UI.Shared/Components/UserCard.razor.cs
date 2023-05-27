using VolunteerManager.Models.Views;
using Microsoft.AspNetCore.Components;

namespace VolunteerManager.UI.Shared.Components;

public partial class UserCard
{
    [Parameter]
    public string Class { get; set; } = string.Empty;

    private string FirstName { get; set; } = string.Empty;

    private string SecondName { get; set; } = string.Empty;

    private string Email { get; set; } = string.Empty;

    private char FirstLetterOfName { get; set; }

    [Parameter]
    public UserView? CurrentUser { get; set; }

    public string? ImageDataUrl { get; set; }

    protected override Task OnInitializedAsync() => LoadDataAsync();

    private Task LoadDataAsync()
    {
        Email = CurrentUser?.Email?.Replace(".com", string.Empty) ?? string.Empty;
        FirstName = CurrentUser?.FirstName ?? string.Empty;
        SecondName = CurrentUser?.LastName ?? string.Empty;

        if (FirstName.Length > 0)
        {
            FirstLetterOfName = FirstName[0];
        }

        ImageDataUrl = CurrentUser?.ImageDataUrl;

        return Task.CompletedTask;
    }
}