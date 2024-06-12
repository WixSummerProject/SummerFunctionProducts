using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SummerFunctionProducts.Models;
using SummerFunctionProducts.Services;

namespace SummerFunctionProducts.Functions
{
    public class CreateProduct
    {
        private readonly ILogger<CreateProduct> _logger;
        private readonly ProductServices _products;

        public CreateProduct(ILogger<CreateProduct> logger, ProductServices products)
        {
            _logger = logger;
            _products = products;
        }

        [Function("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                if (req.Body != null)
                {
                    var httpreq = await new StreamReader(req.Body).ReadToEndAsync();

                    if(httpreq != null)
                    {
                        var json = JsonConvert.DeserializeObject<CreateProductModel>(httpreq);

                        if(json != null)
                        {
                            var result = await _products.CreateProduct(json);

                            switch (result)
                            {
                                case OkResult: return new OkObjectResult(new { Status = 200, Message = "The product is successfully added" });

                                case ConflictResult: return new ConflictObjectResult(new { Status = 409, Message = "Conflict occurred while adding the product" });

                                case BadRequestResult: return new BadRequestObjectResult(new { Status = 400, Message = "Bad request" });

                                default: return new StatusCodeResult(500);
                            }
                        }
                    }
                }

                return new BadRequestObjectResult(new { Status = 400, Message = "Malformed request syntax, invalid request message framing, or deceptive request routing" });
            }
            catch (Exception ex)
            {
                _logger.LogError("C# HTTP trigger function processed a request." + ex.Message);
            }

            return null!; 
        }
    }
}
