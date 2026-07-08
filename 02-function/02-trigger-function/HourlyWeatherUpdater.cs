using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TriggerFunctionApp
{
    public class HourlyWeatherUpdater
    {
        private readonly ILogger<HourlyWeatherUpdater> _logger;

        public HourlyWeatherUpdater(ILogger<HourlyWeatherUpdater> logger)
        {
            _logger = logger;
        }

        [Function("HourlyWeatherUpdater")]
        [CosmosDBOutput(
            databaseName: "WeatherDatabase",
            containerName: "WeatherForecasts",
            Connection = "CosmosDBConnectionSetting")]
        public WeatherRecord? Run(
            // NCRONTAB expression: Runs exactly at the top of every hour
            [TimerTrigger("0 0 * * * *")] TimerInfo myTimer,
            
            // Input Binding: Automatically fetches item with ID "1" on wakeup
            [CosmosDBInput(
                databaseName: "WeatherDatabase",
                containerName: "WeatherForecasts",
                Connection = "CosmosDBConnectionSetting",
                Id = "1",
                PartitionKey = "1")] WeatherRecord? existingRecord)
        {
            _logger.LogInformation($"Timer function executed at: {DateTime.Now}");

            if (existingRecord == null)
            {
                _logger.LogWarning("Record with ID '1' was not found in Cosmos DB.");
                return null;
            }

            // Modify the summary record to verify execution
            existingRecord.Summary = $"Updated by Timer at {DateTime.UtcNow:HH:mm} UTC";
            existingRecord.TemperatureC = new Random().Next(-20, 40); // Random temperature for demonstration
            _logger.LogInformation($"Updating record summary to: {existingRecord.Summary}");

            // Returning the object automatically triggers the CosmosDBOutput commit
            return existingRecord;
        }
    }
}
