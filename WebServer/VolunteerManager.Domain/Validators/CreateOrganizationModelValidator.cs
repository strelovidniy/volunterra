using FluentValidation;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Validators.Extensions;
using VolunteerManager.Models.Create;

namespace VolunteerManager.Domain.Validators;

internal class CreateOrganizationModelValidator : AbstractValidator<CreateOrganizationModel>
{
    public CreateOrganizationModelValidator(IValidationService validationService)
    {
        RuleFor(createOrganizationModel => createOrganizationModel.OrganizationName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.OrganizationNameRequired)
            .MaximumLength(250)
            .WithStatusCode(StatusCode.OrganizationNameTooLong)
            .MustAsync(validationService.IsOrganizationNameUniqueAsync)
            .WithStatusCode(StatusCode.OrganizationNameAlreadyExists);

        RuleFor(createOrganizationModel => createOrganizationModel.OrganizationDescription)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(2000)
            .WithStatusCode(StatusCode.OrganizationDescriptionMustHaveNotMoreThan2000Characters);

        RuleFor(createOrganizationModel => createOrganizationModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .EmailAddress()
            .WithStatusCode(StatusCode.InvalidEmailFormat)
            .MustAsync(validationService.IsEmailUniqueAsync)
            .WithStatusCode(StatusCode.EmailAlreadyExists);

        RuleFor(createOrganizationModel => createOrganizationModel.Password)
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

        RuleFor(createOrganizationModel => createOrganizationModel.ConfirmPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.ConfirmPasswordRequired)
            .Equal(createOrganizationModel => createOrganizationModel.Password)
            .WithStatusCode(StatusCode.PasswordsDoNotMatch);

        RuleFor(createOrganizationModel => createOrganizationModel.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.FirstNameRequired)
            .Must(firstName => !firstName.Contains(" "))
            .WithStatusCode(StatusCode.FirstNameShouldNotContainWhiteSpace);

        RuleFor(createOrganizationModel => createOrganizationModel.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.LastNameRequired)
            .Must(lastName => !lastName.Contains(" "))
            .WithStatusCode(StatusCode.LastNameShouldNotContainWhiteSpace);
    }
}