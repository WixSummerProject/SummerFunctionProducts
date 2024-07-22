using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SummerFunctionProducts.Models;

namespace SummerFunctionProducts.Functions
{
    public class GetSubcategories
    {
        private readonly ILogger<GetSubcategories> _logger;
        private readonly CategoryServices _categoryServices;

        public GetSubcategories(ILogger<GetSubcategories> logger, CategoryServices categoryServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
        }

        [Function("GetSubcategories")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                if (req.Body != null)
                {
                    string httpreq = await new StreamReader(req.Body).ReadToEndAsync();

                    if (httpreq != null)
                    {
                        var json = JsonConvert.DeserializeObject<GetSubcategoriesModel>(httpreq);

                        int intToFind = json.Id;

                        string nameToFInd = json.Name;

                        var response = await _categoryServices.GetSubcategories(intToFind, nameToFInd);

                        if (response != null)
                        {
                            return new OkObjectResult(response);
                        }
                    }
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
