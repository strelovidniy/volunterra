using VolunteerManager.Data.Enums;

namespace VolunteerManager.Domain.Models;

public record CurrencyModel(
    CurrencyCode CurrencyCodeA,
    CurrencyCode CurrencyCodeB,
    decimal RateBuy,
    decimal RateSell
);