using Microsoft.AspNetCore.Identity;

namespace Salyar.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
