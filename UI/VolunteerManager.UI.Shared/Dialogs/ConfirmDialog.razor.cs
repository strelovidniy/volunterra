using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace VolunteerManager.UI.Shared.Dialogs;

public partial class ConfirmDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public string Text { get; set; } = string.Empty;

    protected override Task OnInitializedAsync() => InvokeAsync(StateHasChanged);

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Confirm()
    {
        MudDialog.Close(true);
    }
}