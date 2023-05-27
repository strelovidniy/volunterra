namespace VolunteerManager.UI.Domain.Models;

public record ODataResponse<T>(
    string Context,
    List<T>? Value,
    int Count
);