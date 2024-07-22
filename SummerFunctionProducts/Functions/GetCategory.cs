using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SummerFunctionProducts.Models;
using System.Diagnostics;

namespace SummerFunctionProducts.Functions
{
    public class GetCategory
    {
        private readonly ILogger<GetCategory> _logger;
        private readonly CategoryServices _categoryServices;

        public GetCategory(ILogger<GetCategory> logger, CategoryServices categoryServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
        }

        [Function("GetCategory")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            try
            {

                var httpreq = await new StreamReader(req.Body).ReadToEndAsync();

                if (httpreq != null)
                {
                    var model = JsonConvert.DeserializeObject<CreateCategoryModel>(httpreq);

                    var result = await _categoryServices.GetCategoryAsync(model!.Id);

                    if(result != null)
                    {
                        return new OkObjectResult(result);
                    }

                }

                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new NotFoundResult();
            }
        }
    }
}
