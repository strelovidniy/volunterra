using System.Security.Cryptography;
using System.Text;

namespace VolunteerManager.Domain.Helpers;

public static class PasswordHasher
{
    public static string GetHash(string input)
    {
        using var algorithm = SHA512.Create();

        var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

        var sBuilder = new StringBuilder();

        foreach (var c in data)
        {
            sBuilder.Append(c.ToString("x2"));
        }

        return sBuilder.ToString();
    }
}