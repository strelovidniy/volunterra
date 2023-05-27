namespace VolunteerManager.Domain.Models.ViewModels;

public record NewApplicantEmailViewModel(
    string Url
) : IEmailViewModel;