using System.ComponentModel.DataAnnotations;

namespace SummerFunctionProducts.Models
{
    public class CreateSubcategoryModel
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Kategorinamnet är för långt. (Max 100 tecken)")]
        public string SubcategoryName { get; set; } = null!;

        public int CategoryId { get; set; }
    }
}
