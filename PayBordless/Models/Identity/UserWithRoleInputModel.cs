using System.ComponentModel.DataAnnotations;

namespace PayBordless.Models.Identity;

public class UserWithRoleInputModel
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    public string Role { get; set; }

    [Required]
    public string Password { get; set; }
}