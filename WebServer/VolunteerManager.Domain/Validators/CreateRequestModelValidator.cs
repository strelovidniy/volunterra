using FluentValidation;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Validators;

internal class CreateRequestModelValidator : AbstractValidator<CreateOrganizationRequestReplyModel>
{
}