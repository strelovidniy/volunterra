using FluentValidation;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Domain.Validators.Extensions;

internal static class FluentValidationExtension
{
    public static IRuleBuilderOptions<T, TProperty> WithStatusCode<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        StatusCode statusCode
    ) => rule.WithErrorCode(((int) statusCode).ToString());
}