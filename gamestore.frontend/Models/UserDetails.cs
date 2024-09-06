using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class UserDetails
{
    [DataType(DataType.EmailAddress)]
    [StringLength(128)]
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "Password must be between 10 and 50 characters.", MinimumLength = 10)]
    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }
}