using System.ComponentModel.DataAnnotations;

namespace PayBordless.Models.Identity;

public class UserLoginModel
{
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}