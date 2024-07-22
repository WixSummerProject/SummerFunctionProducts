using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerFunctionProducts.Models;
using SummerFunctionProducts.Factory;
using SummerFunctionProducts.Functions;

public class CategoryServices
{
    private readonly ProductContext _context;
    private readonly ConvertCategory _factory;

    public CategoryServices(ProductContext context, ConvertCategory factory)
    {
        _context = context;
        _factory = factory;
    }

    public async Task<IActionResult> GetCategoriesAsync()
    {
        try
        {
            List<CategoryEntity> categories = _context.Categories.ToList();

            var categorieList = _factory.ConvertCategories(categories);

            return new OkObjectResult(categorieList); 
        }
        catch (Exception ex)
        {
            return new StatusCodeResult(500);
        }
    }

    public async Task<CreateCategoryModel> GetCategoryAsync(int id)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            var categorieList = _factory.ConvertACategory(category);

            return categorieList;
        }
        catch (Exception ex)
        {
            return null!;
        }
    }

    public async Task<CreateSubcategoryModel> GetSubcategoryAsync(string name)
    {
        try
        {
            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(x => x.SubategoryName == name);

            if(subcategory != null)
            {
                var subcategoryModel = _factory.ConvertASubcategory(subcategory);

                return subcategoryModel;
            }

            return null!;

        }
        catch (Exception ex)
        {
            return null!;
        }
    }

    public async Task<List<CreateSubcategoryModel>> GetSubcategories(int id, string name)
    {
        try
        {
            if(id != 0)
            {
                List<SubcategoryEntity> subcategories = _context.Subcategories.Where(x => x.CategoryId == id).ToList();

                var subcategorieList = await _factory.ConvertSubcategory(subcategories);

                return subcategorieList;
            }
            if(name != null)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == name);

                var categoryId = category.Id;

                List<SubcategoryEntity> subcategories = _context.Subcategories.Where(x => x.CategoryId == categoryId).ToList();

                var subcategorieList = await _factory.ConvertSubcategory(subcategories);

                return subcategorieList;
            }
            else
            { return null!; }   

        }
        catch (Exception ex)
        {
            return null!;
        }
    }
}
