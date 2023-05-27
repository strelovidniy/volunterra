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
        //add validation for new entity  
    }
}