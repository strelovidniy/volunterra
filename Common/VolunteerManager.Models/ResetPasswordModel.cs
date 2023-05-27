namespace VolunteerManager.Models;

public record ResetPasswordModel(
    string Email
) : IValidatableModel;