using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models;

namespace VolunteerManager.Domain.Validators;

internal class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidator(IValidationService validationService)
    {
        RuleFor(resetPasswordModel => resetPasswordModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .EmailAddress()
            .WithStatusCode(StatusCode.InvalidEmailFormat)
            .MustAsync(validationService.IsUserExistsAsync)
            .WithStatusCode(StatusCode.UserNotFound);
    }
}