using System.ComponentModel.DataAnnotations;

namespace JWTtest.Models
{
    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; }
        [Required]

        public string Role { get; set; }
    }
}
