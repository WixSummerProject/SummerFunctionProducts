using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; } = null!;

        public List<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();
    }
}
