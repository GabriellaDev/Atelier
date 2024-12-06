using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtelierShared.Models
{
    [Table("InitiativePosts")]

    public class InitiativePost
    {
        public int Id { get; set; } // Unified primary key

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        public int AuthorId { get; set; } // Foreign key to User

        public DateTime DatePublished { get; set; } = DateTime.UtcNow; // Default value

        public int CategoryId { get; set; } // Foreign key to CategoryModel
        public CategoryModel? Category { get; set; }
    }
}
