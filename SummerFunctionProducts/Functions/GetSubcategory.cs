using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SummerFunctionProducts.Models;

namespace SummerFunctionProducts.Functions
{
    public class GetSubcategory
    {
        private readonly ILogger<GetSubcategory> _logger;
        private readonly CategoryServices _categoryServices;

        public GetSubcategory(ILogger<GetSubcategory> logger, CategoryServices categoryServices)
        {
            _logger = logger;
            _categoryServices = categoryServices;
        }

        [Function("GetSubcategory")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            try
            {
                if(req.Body != null)
                {
                    var model = await new StreamReader(req.Body).ReadToEndAsync();

                    if(model != null)
                    {
                        var json = JsonConvert.DeserializeObject<CreateSubcategoryModel>(model);

                        string nameToFind = json.SubcategoryName; 

                        if(json != null)
                        {
                            var result = await _categoryServices.GetSubcategoryAsync(nameToFind);

                            if(result != null)
                            {
                                return new OkObjectResult(result);
                            }
                        }
                    }
                }

                return new BadRequestResult();
            }
            catch (Exception ex) 
            {

                return new BadRequestResult();
            }
        }
    }
}
