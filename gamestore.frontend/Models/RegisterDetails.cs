using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class RegisterDetails
{
    [DataType(DataType.EmailAddress)]
    [StringLength(128)]
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "Password must be between 10 and 50 characters.", MinimumLength = 10)]
    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one number.")]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [CompareProperty("Password", ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }
}