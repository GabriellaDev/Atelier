namespace AtelierShared.Models
{
    public class RegisterRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "User"; // Default to "User"
    }
}
