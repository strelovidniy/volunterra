using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VolunteerManager.Data.Entities;
using VolunteerManager.Data.Enums;
using VolunteerManager.Domain.Extensions;
using VolunteerManager.Domain.Helpers;
using VolunteerManager.Domain.Services.Abstraction;
using VolunteerManager.Domain.Settings.Abstraction;
using VolunteerManager.Domain.Validators.Runtime;
using VolunteerManager.Models;
using VolunteerManager.Models.Views;

namespace VolunteerManager.Domain.Services.Realization;

internal class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;
    private readonly IJwtSettings _jwtSettings;
    private readonly IMapper _mapper;

    public AuthService(
        IRepository<User> userRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService,
        IJwtSettings jwtSettings,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
        _jwtSettings = jwtSettings;
        _mapper = mapper;
    }

    public Task<UserView?> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    ) => _userService.GetUserAsync(
        _httpContextAccessor.GetCurrentUserId(),
        cancellationToken
    );

    public async Task<LoginView?> LoginAsync(
        LoginModel model,
        CancellationToken cancellationToken = default
    )
    {
        var user = await _userRepository
            .NoTrackingQuery()
            .Include(user => user.Organization)
            .FirstOrDefaultAsync(
                u => u.Email == model.Email,
                cancellationToken
            );

        RuntimeValidator.Assert(user is not null, StatusCode.AccountNotFound);

        RuntimeValidator.Assert(
            user!.PasswordHash == PasswordHasher.GetHash(model.Password),
            StatusCode.IncorrectPassword
        );

        if (user.Organization is not null)
        {
            user.Organization.Users = new List<User>();
        }

        var refreshToken = Guid.NewGuid();
        var refreshTokenExpireAt = DateTime.UtcNow.AddSeconds(_jwtSettings.SecondsToExpireRefreshToken);

        user!.RefreshToken = refreshToken;
        user.RefreshTokenExpirationDate = refreshTokenExpireAt;

        await _userRepository.SaveChangesAsync(cancellationToken);

        var authTokenModel = GenerateToken(user);

        authTokenModel.RefreshToken = refreshToken;

        return new LoginView
        {
            User = _mapper.Map<UserView>(user),
            Token = authTokenModel
        };
    }

    private AuthToken GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
            //new(ClaimTypes.Role, user.Role?.Name ?? string.Empty)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.SecondsToExpireToken),
            Issuer = _jwtSettings.Issuer,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_jwtSettings.TokenSecretString.ToByteArray()),
                SecurityAlgorithms.HmacSha512Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthToken
        {
            Token = tokenHandler.WriteToken(token),
            ExpireAt = token.ValidTo.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")
        };
    }
}