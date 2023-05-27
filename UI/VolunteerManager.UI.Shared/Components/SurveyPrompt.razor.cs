using Microsoft.AspNetCore.Components;

namespace VolunteerManager.UI.Shared.Components;

public partial class SurveyPrompt
{
    // Demonstrates how a parent component can supply parameters
    [Parameter]
    public string? Title { get; set; }
}