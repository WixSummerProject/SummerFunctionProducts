using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ProductEntity
    {
        [Key]
        public string Id { get; set; } = null!;

        [MaxLength(100)]
        public string Headline { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public string? Category {  get; set; }

        public bool SellBuy { get; set; } = false;

        public List<ImageEntity>? Images { get; set; }

        [MaxLength(100)]
        public string Price { get; set; } = null!;
        
        [MaxLength(100)]

        public string Place { get; set; } = null!;

        public string DateCreated { get; set; } = null!;

    }
}
