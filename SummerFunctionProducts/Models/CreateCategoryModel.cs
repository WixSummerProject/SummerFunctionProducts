using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SummerFunctionProducts.Models
{
    public class CreateCategoryModel
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Kategorinamnet är för långt. (Max 100 tecken.)")]
        public string CategoryName { get; set; } = null!;

        public List<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();
    }
}
