using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Validators;

internal class CreateOrganizationRequestModelValidation : AbstractValidator<CreateOrganizationRequestModel>
{
    public CreateOrganizationRequestModelValidation(IValidationService validationService)
    {
        RuleFor(createOrganizationRequestModel => createOrganizationRequestModel.RequestId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(validationService.IsRequestExistAsync)
            .WithStatusCode(StatusCode.RequestNotFound);

        RuleFor(createOrganizationRequestModel => createOrganizationRequestModel.ServedCount)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithStatusCode(StatusCode.ServedCountMustBeGreaterThanOrEqualToZero);
    }
}