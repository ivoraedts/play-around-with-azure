using System.Text.Json.Serialization;

namespace WeatherFunctionApp
{
    public class WeatherRecord
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("temperatureC")]
        public int TemperatureC { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;
    }
}
