using System.ComponentModel.DataAnnotations;

namespace AskMawaddaJWT.Models;

public class RegisterModel
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = default!;
    [Required, StringLength(100)]
    public string LastName { get; set; } = default!;
    [Required, StringLength(50)]
    public string UserName { get; set; } = default!;
    [Required, StringLength(128)]
    public string Email { get; set; } = default!;
    [Required, StringLength(256)]
    public string Password { get; set; } = default!;
}
