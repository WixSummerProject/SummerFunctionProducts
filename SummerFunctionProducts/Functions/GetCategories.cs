using Data.Entities;
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
    public class GetCategories
    {
        private readonly ILogger<GetCategories> _logger;
        private readonly CategoryServices _categoryServices;

        public GetCategories(ILogger<GetCategories> logger, CategoryServices categoryServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
        }

        [Function("GetCategories")]
        [Authorize]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            try 
            {
                var response = await _categoryServices.GetCategoriesAsync();

                if(response != null)
                {
                    return new OkObjectResult(response);
                }

                return new NotFoundResult();
            }
            catch (Exception ex) 
            {
                return new NotFoundResult();
            }
        }
    }
}
