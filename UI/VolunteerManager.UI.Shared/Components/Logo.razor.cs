using Microsoft.AspNetCore.Components;

namespace VolunteerManager.UI.Shared.Components;

public partial class Logo
{
    [Parameter]
    public string? Class { get; set; } = string.Empty;
}