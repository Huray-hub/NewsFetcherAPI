namespace NewsFetcherAPI.Models
{
    public class NewsApiSettings
    {
        public string Url { get; set; }
        public string EndPoint { get; set; }
        public string ApiKey { get; set; }
        public int? PageSize { get; set; }
    }
}