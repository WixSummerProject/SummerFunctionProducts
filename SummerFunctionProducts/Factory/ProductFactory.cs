using Data.Entities;
using SummerFunctionProducts.Models;

namespace Data.Factory
{
    public class ProductFactory
    {
        public async Task<ProductEntity> ConvertProduct(CreateProductModel model)
        {
            bool sell = false;

            if (model.SellBuy == "sell")
            {
                sell = false;
            }
            else if (model.SellBuy == "buy")
            {
                sell = true;
            }

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
                SellBuy = sell,
        };

            return productEntity;
        }
    }
}
