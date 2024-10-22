using System.ComponentModel.DataAnnotations;

namespace AskMawaddaJWT.Models;

public class TokenRequestModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}
