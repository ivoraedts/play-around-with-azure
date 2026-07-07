using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace WeatherFunctionApp
{
    public class AddWeatherForecast
    {
        private readonly ILogger<AddWeatherForecast> _logger;

        public AddWeatherForecast(ILogger<AddWeatherForecast> logger)
        {
            _logger = logger;
        }

        [Function("AddWeatherForecast")]
        public async Task<MultiResponse> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "weather")] HttpRequest req)
        {
            _logger.LogInformation("Processing a request to add a weather forecast record...");

            // 1. Read the JSON body using standard ASP.NET Core HttpRequest
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherRecord>(requestBody);

            if (weatherData == null || string.IsNullOrEmpty(weatherData.Id))
            {
                return new MultiResponse 
                { 
                    HttpResponse = new BadRequestObjectResult("Invalid weather data provided.") 
                };
            }

            // 2. Return the combined response (HTTP Status 200 + Cosmos DB Output)
            return new MultiResponse
            {
                CosmosOutput = weatherData,
                HttpResponse = new OkObjectResult(weatherData)
            };
        }
    }

    public class MultiResponse
    {
        [CosmosDBOutput(
            databaseName: "WeatherDatabase",
            containerName: "WeatherForecasts",
            Connection = "CosmosDBConnectionSetting")]
        public WeatherRecord? CosmosOutput { get; set; }

        [HttpResult]
        public IActionResult? HttpResponse { get; set; }
    }
}
