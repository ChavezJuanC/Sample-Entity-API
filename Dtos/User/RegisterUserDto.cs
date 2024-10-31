using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "UserName must be 3-20 characters.")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

