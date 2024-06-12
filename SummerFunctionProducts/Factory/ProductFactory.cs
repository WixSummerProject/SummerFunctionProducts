using Data.Entities;
using SummerFunctionProducts.Models;

namespace Data.Factory
{
    public class ProductFactory
    {
        public async Task<ProductEntity> ConvertProduct(CreateProductModel model)
        {
            var productEntity = new ProductEntity
            {
                Id = model.Id,
                Headline = model.Headline,
                Category = model.Category,
                Description = model.Description,
                DateCreated = model.DateCreated,
                Images = model.Images,
                Place = model.Place,
                Price = model.Price,
                SellBuy = model.SellBuy,
            };

            return productEntity;
        }
    }
}
