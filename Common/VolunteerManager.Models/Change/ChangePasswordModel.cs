namespace VolunteerManager.Models.Change;

public class ChangePasswordModel : IValidatableModel
{
    public string OldPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;

    public string ConfirmNewPassword { get; set; } = null!;
}