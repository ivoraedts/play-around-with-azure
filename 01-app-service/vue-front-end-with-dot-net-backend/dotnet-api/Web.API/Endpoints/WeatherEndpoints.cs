using Microsoft.Azure.Cosmos;

namespace Web.API.Endpoints;

public static class WeatherEndpoints
{
    // This extension method hooks directly onto the WebApplication builder ring
    public static void MapWeatherEndpoints(this WebApplication app, string dbId, string containerId)
    {
        app.MapGet("/weatherforecast", async (CosmosClient client) =>
        {
            var container = client.GetContainer(dbId, containerId);
            var queryDefinition = new QueryDefinition("SELECT * FROM c");
            
            using FeedIterator<WeatherForecast> queryResultSetIterator = 
                container.GetItemQueryIterator<WeatherForecast>(queryDefinition);

            var forecasts = new List<WeatherForecast>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<WeatherForecast> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (WeatherForecast forecast in currentResultSet)
                {
                    forecasts.Add(forecast);
                }
            }

            return Results.Ok(forecasts);
        })
        .WithName("GetWeatherForecast");
    }
}

// Keep the data record template neatly at the bottom of the namespace
public record WeatherForecast(string id, string date, int temperatureC, string summary);
