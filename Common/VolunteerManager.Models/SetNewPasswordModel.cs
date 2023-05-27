namespace VolunteerManager.Models;

public class SetNewPasswordModel : IValidatableModel
{
    public Guid VerificationCode { get; set; }

    public string NewPassword { get; set; } = null!;

    public string ConfirmNewPassword { get; set; } = null!;
}