using System.Text.Json.Serialization;

namespace NewsFetcherAPI.Models
{
    public class NewsApiResponseBase
    {
        [JsonPropertyName("status")] public string Status { get; set; }
    }
}