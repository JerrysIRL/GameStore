using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class UserDetails
{
    [Required(ErrorMessage = "Email is required.")]
    [StringLength(128)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "Password must be between 10 and 50 characters.")]
    public string Password { get; set; } = "";
}