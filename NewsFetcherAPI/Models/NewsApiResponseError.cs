using Newtonsoft.Json;

namespace NewsFetcherAPI.Models
{
    public class NewsApiResponseError : NewsApiResponseBase
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
    }
}