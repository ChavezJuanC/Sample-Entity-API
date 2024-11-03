using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User
{
    public class LogInDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}