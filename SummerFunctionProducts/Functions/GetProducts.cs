using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SummerFunctionProducts.Services;
using System.Diagnostics;

namespace SummerFunctionProducts.Functions
{
    public class GetProducts
    {
        private readonly ILogger<GetProducts> _logger;
        private readonly ProductServices _productServices;

        public GetProducts(ILogger<GetProducts> logger, ProductServices productServices)
        {
            _logger = logger;
            _productServices = productServices;
        }

        [Function("GetProducts")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            try
            {
                using (StreamReader reader = new StreamReader(req.Body))
                {
                    var httpreq = await reader.ReadToEndAsync();

                    if (httpreq != null)
                    {
                        var listToSend = _productServices.GetProductsAsync();

                        return new OkObjectResult(listToSend);
                    }

                    return new NotFoundResult();
                }

            }
            catch (Exception ex) 
            { _logger.LogError(ex.Message); return null!; }
        }
    }
}
