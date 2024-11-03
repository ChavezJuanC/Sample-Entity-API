namespace api.Dtos.User
{
    public class LoggedInUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}