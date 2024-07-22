using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class SubcategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string SubategoryName { get; set; } = null!;

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryEntity Category { get; set; } = null!;

    }
}
