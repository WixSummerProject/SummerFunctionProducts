using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using SummerFunctionProducts.Services;
using Data.Factory;
using SummerFunctionProducts.Factory;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<ProductContext>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("SqlServer")));
        services.AddScoped<ProductServices>();
        services.AddScoped<ProductFactory>();
        services.AddScoped<CategoryServices>();
        services.AddScoped<ConvertCategory>();
    })
    .Build();

host.Run();
