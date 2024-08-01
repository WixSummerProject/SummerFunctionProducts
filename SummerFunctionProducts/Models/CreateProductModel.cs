using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SummerFunctionProducts.Models
{
    public class CreateProductModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(100, ErrorMessage = "Rubriken är för lång. (Max 100 tecken)")]
        public string Headline { get; set; } = null!;

        [MaxLength(1000, ErrorMessage = "Texten är för lång. (Max 1000 tecken)")]
        public string? Description { get; set; }

        public string? Category { get; set; }

        public string SellBuy { get; set; } = null!;

        public List<string>? Images { get; set; }

        [MaxLength(100)]
        public string Price { get; set; } = null!;

        [MaxLength(100)]

        public string Place { get; set; } = null!;

        public string DateCreated { get; set; } = DateTime.Now.ToString();
    }
}
