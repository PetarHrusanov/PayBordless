namespace PayBordless.Models.Identity;

using System.ComponentModel.DataAnnotations;

public class UserInputModel
{
    [Required]
    public string Name { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}