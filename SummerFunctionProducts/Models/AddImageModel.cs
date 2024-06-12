using System.ComponentModel.DataAnnotations;

namespace SummerFunctionProducts.Models
{
    public class AddImageModel
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ImageUrl { get; set; } = null!;
    }
}
