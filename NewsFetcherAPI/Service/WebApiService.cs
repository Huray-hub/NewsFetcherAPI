using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NewsFetcherAPI.Models;

namespace NewsFetcherAPI.Service
{
    public class WebApiService
    {
        private readonly string _url;
        private readonly string _apiKey;

        public WebApiService(string url, string apiKey)
        {
            _url = url;
            _apiKey = apiKey;
        }
        
        public async Task<TResponse> Get<TResponse>(string category) where TResponse : NewsApiResponseBase =>
            await PrepareHttpRequest<TResponse, RequestBase>(category, HttpMethod.Get);
        
        private async Task<TResponse> PrepareHttpRequest<TResponse, TRequest>(string category, HttpMethod method,
            TRequest request = null) where TResponse : NewsApiResponseBase where TRequest : RequestBase
        {
            var finalUrl = _url + category;

            var requestMessage = new HttpRequestMessage(method, finalUrl);
            requestMessage.Headers.Add("X-Api-Key", _apiKey);

            if (request is null || method == HttpMethod.Get)
                return await SendHttpRequest<TResponse>(requestMessage);

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            requestMessage.Content = content;

            return await SendHttpRequest<TResponse>(requestMessage);
        }

        private async Task<T> SendHttpRequest<T>(HttpRequestMessage requestMessage) where T : NewsApiResponseBase
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.ConnectionClose = true;

            try
            {
                var response = await client.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();
                return await HandleHttpResponse<T>(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                //TODO:Error handling
            }
        }

        private async Task<T> HandleHttpResponse<T>(HttpResponseMessage response) where T : NewsApiResponseBase
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var result = JsonSerializer.Deserialize<T>(responseContent);
            return result;
        }
    }
}