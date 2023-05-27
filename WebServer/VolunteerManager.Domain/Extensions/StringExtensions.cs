using System.Text;

namespace VolunteerManager.Domain.Extensions;

public static class StringExtensions
{
    public static byte[] ToByteArray(this string value) => Encoding.ASCII.GetBytes(value);
}