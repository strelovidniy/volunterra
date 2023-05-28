using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Validators;

internal class CreateRequestModelValidator : AbstractValidator<CreateOrganizationRequestReplyModel>
{
    public CreateRequestModelValidator()
    {
      
    }
}