using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SummerFunctionProducts.Models;
using SummerFunctionProducts.Services;

namespace SummerFunctionProducts.Functions
{
    public class DeleteProduct
    {
        private readonly ILogger<DeleteProduct> _logger;
        private readonly ProductServices _productService;

        public DeleteProduct(ILogger<DeleteProduct> logger, ProductServices productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Function("DeleteProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                if (req.Body != null)
                {
                    var delete = await new StreamReader(req.Body).ReadToEndAsync();

                    if(delete != null)
                    {
                        var json = JsonConvert.DeserializeObject<DeleteProductModel>(delete);
                        var result = await _productService.DeleteAsync(json);
                        return new OkObjectResult(new { Status = 200, Message = "The product is successfully deleted" });
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
