using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtelierShared.Models

{
    [Table("Category")]

    public class CategoryModel
    {
        public int Id { get; set; } // Unified primary key
        [Required]
        public string? Category { get; set; } = string.Empty;
    }

}