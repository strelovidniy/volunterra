namespace VolunteerManager.Domain.Extensions;

public static class EnumerableExtensions
{
    public static T[] ToNullSafeArray<T>(this IEnumerable<T>? source) =>
        source is not null
            ? source as T[] ?? source.ToArray()
            : Array.Empty<T>();
}