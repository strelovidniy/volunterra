using Humanizer;
using VolunteerManager.Data.Enums;

namespace VolunteerManager.Models;

public class ApiErrorResult
{
    public int StatusCode { get; init; }

    public string Message { get; init; }

    public ApiErrorResult(StatusCode statusCode)
    {
        StatusCode = (int) statusCode;
        Message = statusCode.ToString().Humanize();
    }
}