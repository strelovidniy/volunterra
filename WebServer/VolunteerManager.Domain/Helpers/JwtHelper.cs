using Microsoft.IdentityModel.Tokens;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Domain.Settings.Abstraction;

namespace VolunteerManager.Domain.Helpers;

public static class JwtHelper
{
    public static TokenValidationParameters GetTokenValidationParameters(IJwtSettings jwtSettings) =>
        new()
        {
            ValidateIssuer = true,
            ValidIssuers = new[] { jwtSettings.Issuer },

            ValidateAudience = false,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.TokenSecretString.ToByteArray()),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
}