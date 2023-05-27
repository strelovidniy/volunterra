using VolunteerManager.Data.Enums;

namespace VolunteerManager.Domain.Exceptions;

public class ApiException : Exception
{
    public StatusCode StatusCode { get; }

    public ApiException(StatusCode statusCode) => StatusCode = statusCode;
}