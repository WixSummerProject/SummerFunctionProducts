using Data.Entities;
using SummerFunctionProducts.Models;

namespace SummerFunctionProducts.Factory
{
    public class ConvertCategory
    {
        public async Task<List<CreateCategoryModel>> ConvertCategories(List<CategoryEntity> entity)
        {
            var categories = new List<CreateCategoryModel>();

            foreach (var category in entity) 
            {
                var categoryModel = new CreateCategoryModel
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                    Subcategories = category.Subcategories,
                };

                categories.Add(categoryModel);
            }

            return categories;
        }

        public CreateCategoryModel ConvertACategory(CategoryEntity entity)
        {
            var model = new CreateCategoryModel
            { 
                Id = entity.Id,
                CategoryName = entity.CategoryName 
            };

            return model;
        }

        public CreateSubcategoryModel ConvertASubcategory(SubcategoryEntity entity)
        {
            var model = new CreateSubcategoryModel
            {
                CategoryId = entity.CategoryId,
                Id = entity.Id,
                SubcategoryName = entity.SubategoryName,
            };

            return model;
        }

        public async Task<List<CreateSubcategoryModel>> ConvertSubcategory(List<SubcategoryEntity> entity)
        {
            var subcategories = new List<CreateSubcategoryModel>();

            foreach (var item in entity)
            {
                var subcategoryModel = new CreateSubcategoryModel
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    SubcategoryName = item.SubategoryName
                };

                subcategories.Add(subcategoryModel);
            }

            return subcategories;
        }
    }
}
