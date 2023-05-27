using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Change;

namespace VolunteerManager.Domain.Validators;

internal class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator(IValidationService validationService)
    {
        RuleFor(changePasswordModel => changePasswordModel.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.PasswordRequired)
            .MinimumLength(8)
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeast8Characters)
            .MaximumLength(32)
            .WithStatusCode(StatusCode.PasswordMustHaveNotMoreThan32Characters)
            .Matches("[A-Z]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneUppercaseLetter)
            .Matches("[a-z]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneLowercaseLetter)
            .Matches("[0-9]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneDigit);

        RuleFor(changePasswordModel => changePasswordModel.ConfirmNewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.ConfirmPasswordRequired)
            .Equal(changePasswordModel => changePasswordModel.NewPassword)
            .WithStatusCode(StatusCode.PasswordsDoNotMatch);

        RuleFor(changePasswordModel => changePasswordModel.OldPassword)
            .Cascade(CascadeMode.Stop)
            .MustAsync(validationService.IsCurrentUserPasswordCorrectAsync)
            .WithStatusCode(StatusCode.OldPasswordIncorrect);
    }
}