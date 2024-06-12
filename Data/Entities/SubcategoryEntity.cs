using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class SubcategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string SubategoryName { get; set; } = null!;
        
    }
}
