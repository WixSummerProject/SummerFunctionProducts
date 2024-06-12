﻿
using Data.Contexts;
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
    }
}