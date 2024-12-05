using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtelierShared.Models
{
    [Table("TravelPosts")]

    public class TravelPost
    {
        public int Id { get; set; } // Unified primary key

        [Required]
        public string? Title { get; set; }

        public string Country { get; set; } = "Unknown"; // Default value

        [Required]
        public string? Content { get; set; }

        public int AuthorId { get; set; } // Foreign key to User

        public DateTime DatePublished { get; set; } = DateTime.UtcNow; // Default value
    }
}