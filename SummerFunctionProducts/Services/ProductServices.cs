
using Data.Contexts;
using Data.Entities;
using Data.Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SummerFunctionProducts.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SummerFunctionProducts.Services
{
    public class ProductServices
    {
        //CRUD
        private readonly ProductContext _context;
        private readonly ProductFactory _factory;

        public ProductServices(ProductContext context, ProductFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        //Create
        public async Task<IActionResult> CreateProduct(CreateProductModel model)
        {
            try
            {
                if(model != null)
                {
                    var exists = await ProductExistsAsync(model);

                    if(exists is OkResult okResult)
                    {
                        var entityToCreate = await _factory.ConvertProduct(model);

                        if (entityToCreate != null)
                        {
                            var result = await _context.AddAsync(entityToCreate);
                            await _context.SaveChangesAsync();
                            return new OkResult();
                        }
                    }
                    if(exists is ConflictResult conflict)
                    {
                        return new ConflictResult();
                    }
                }

                return new BadRequestResult();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return null!; 
            }
        }

        //Exists

        public async Task<IActionResult> ProductExistsAsync(CreateProductModel model)
        {
            try
            {
                if (model.Headline != null)
                {
                    var result = await _context.Products.FirstOrDefaultAsync(x => x.Headline == model.Headline);

                    if(result != null)
                    {
                        return new ConflictResult();
                    }

                     return new OkResult();
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        //Delete
        public async Task<IActionResult> DeleteAsync(DeleteProductModel delete)
        {
            try
            {
                if (delete != null)
                {
                    var result = await _context.Products.FirstOrDefaultAsync(x => x.Headline == delete.Headline|| x.Id == delete.Id);

                    if(result != null)
                    {
                        _context.Products.Remove(result);
                        await _context.SaveChangesAsync();
                        return new OkResult();
                    }
                }

                return new BadRequestResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }

        // Get All Products
        public async Task<List<CreateProductModel>> GetProductsAsync()
        {
            try
            {
                List<ProductEntity> products = _context.Products.ToList();

                if(products != null && products.Any())
                {
                    var listToReturn = new List<CreateProductModel>();

                    foreach (var product in products)
                    {
                        var productModel = new CreateProductModel
                        {
                            DateCreated = product.DateCreated,
                            Description = product.Description,
                            Category = product.Category,
                            Headline = product.Headline,
                            Images = product.ImageUrl,
                            Place = product.Place,
                            Id = product.Id,
                            Price = product.Price,
                            SellBuy = product.SellBuy.ToString(),
                        };

                        listToReturn.Add(productModel);
                    }

                    return listToReturn;
                }

                return null!;
            }
            catch (Exception ex){ Debug.WriteLine(ex.Message); return null!; }
        }
    }
}
