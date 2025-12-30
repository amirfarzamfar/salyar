using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Salyar.Application.DTOs.Auth;
using Salyar.Application.Interfaces;
using Salyar.Domain.Constants;
using Salyar.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Salyar.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // 1. Validate Role
        if (!UserRoles.All.Contains(request.Role))
        {
            throw new ArgumentException($"Invalid role: {request.Role}");
        }

        // 2. Check if user exists
        var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (existingUser != null)
        {
            throw new Exception("User with this phone number already exists.");
        }

        // 3. Create User
        var user = new ApplicationUser
        {
            UserName = request.PhoneNumber, // Using Phone as Username
            Email = !string.IsNullOrEmpty(request.Email) ? request.Email : $"{request.PhoneNumber}@salyar.local",
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Registration failed: {errors}");
        }

        // 4. Assign Role
        await _userManager.AddToRoleAsync(user, request.Role);

        // 5. Generate Token
        var token = await GenerateJwtToken(user);

        return new AuthResponse
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            FullName = $"{user.FirstName} {user.LastName}",
            Token = token,
            Roles = new List<string> { request.Role }
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        ApplicationUser? user;
        
        if (request.Identifier.Contains("@"))
        {
            user = await _userManager.FindByEmailAsync(request.Identifier);
        }
        else
        {
            user = await _userManager.FindByNameAsync(request.Identifier);
        }

        if (user == null)
        {
            throw new Exception("Invalid credentials.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            throw new Exception("Invalid credentials.");
        }

        var token = await GenerateJwtToken(user);
        var roles = await _userManager.GetRolesAsync(user);

        return new AuthResponse
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            FullName = $"{user.FirstName} {user.LastName}",
            Token = token,
            Roles = roles
        };
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user == null)
        {
            // For security, don't reveal if user exists, but for now we throw to handle in controller
            // In production, we should just return success and send SMS if user exists
            throw new Exception("کاربری با این شماره یافت نشد.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // TODO: Send token via SMS
        return token;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (user == null)
        {
            throw new Exception("کاربری یافت نشد.");
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"تغییر رمز عبور انجام نشد: {errors}");
        }

        return true;
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryMinutes"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
