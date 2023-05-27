using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models;

namespace VolunteerManager.Domain.Validators;

internal class InviteUserToOrganizationModelValidator : AbstractValidator<InviteUserToOrganizationModel>
{
    public InviteUserToOrganizationModelValidator(IValidationService validationService)
    {
        RuleFor(inviteUserToOrganizationModel => inviteUserToOrganizationModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .EmailAddress()
            .WithStatusCode(StatusCode.InvalidEmailFormat);

        RuleFor(inviteUserToOrganizationModel => inviteUserToOrganizationModel.OrganizationId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(validationService.IsOrganizationExistAsync)
            .WithStatusCode(StatusCode.OrganizationNotFound);
    }
}