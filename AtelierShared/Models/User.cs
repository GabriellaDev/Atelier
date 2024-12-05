using System.ComponentModel.DataAnnotations;

namespace AtelierShared.Models
{
    public class User
    {
        public int Id { get; set; } // Unified primary key

        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty; // Optional field, default value

        [Required]
        public string? Password { get; set; }

        public string Role { get; set; } = "User"; // Default role

        public DateTime DateJoined { get; set; } = DateTime.UtcNow; // Default value
    }
}
