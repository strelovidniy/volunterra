using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Update;

namespace VolunteerManager.Domain.Validators;

internal class UpdateOrganizationModelValidator : AbstractValidator<UpdateOrganizationModel>
{
    public UpdateOrganizationModelValidator(IValidationService validationService)
    {
        RuleFor(updateOrganizationModel => updateOrganizationModel.OrganizationId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(validationService.IsOrganizationExistAsync)
            .WithStatusCode(StatusCode.OrganizationNotFound);

        RuleFor(updateOrganizationModel => updateOrganizationModel.Description)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(2000)
            .WithStatusCode(StatusCode.OrganizationDescriptionMustHaveNotMoreThan2000Characters);
    }
}