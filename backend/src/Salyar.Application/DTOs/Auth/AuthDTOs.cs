using System.ComponentModel.DataAnnotations;

namespace Salyar.Application.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty; // Must be one of UserRoles
}

public class LoginRequest
{
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class ForgotPasswordRequest
{
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
}

public class ResetPasswordRequest
{
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;
}

public class AuthResponse
{
    public string Id { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public IList<string> Roles { get; set; } = new List<string>();
}
