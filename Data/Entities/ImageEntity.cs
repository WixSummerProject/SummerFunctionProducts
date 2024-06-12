using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ImageEntity
    {
        [Key]
        public string Id { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
