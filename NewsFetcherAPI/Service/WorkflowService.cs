using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NewsFetcherAPI.Models;
using NewsFetcherAPI.ViewModels;

namespace NewsFetcherAPI.Service
{
    public class WorkflowService : IWorkflowService
    {
        private readonly INfDbService _dbService;
        private readonly WebApiService _webApiService;
        private readonly NewsApiSettings _newsApiSettings;

        public WorkflowService(IOptions<NewsApiSettings> newsApiSettingsAccessor,
            INfDbService dbService)
        {
            _dbService = dbService;
            _newsApiSettings = newsApiSettingsAccessor.Value;

            var url = PrepareUrl();

            _webApiService = new WebApiService(url, _newsApiSettings.ApiKey);
        }

        public async Task<List<CategoryModel>> GetCategories()
        {
            var entities = await _dbService.GetCategories();

            var models = entities.Select(x => new CategoryModel
            {
                Name = x.Name,
                Value = x.Value
            }).ToList();

            return models;
        }


        public async Task<List<ArticleModel>> GetArticlesByCategory(string category)
        {
            var response = await _webApiService.Get<NewsApiResponseSuccess>(category);
            
            var result = response.Articles.Select(x =>
                new ArticleModel
                {
                    Title = x.Title,
                    Source = string.IsNullOrEmpty(x.Source?.Name) ? "Unknown" : x.Source.Name,
                    Date = x.PublishedAt.ToString("dd/MM"),
                    Url = x.Url
                }).ToList();

            return result;
        }

        private string PrepareUrl()
        {
            var sb = new StringBuilder();
            sb.Append(_newsApiSettings.Url);
            sb.Append('/');
            sb.Append(_newsApiSettings.EndPoint);
            sb.Append('?');

            if (_newsApiSettings.PageSize is not null)
                sb.Append($"pageSize={_newsApiSettings.PageSize}");

            sb.Append('&');
            sb.Append("category=");

            return sb.ToString();
        }
    }
}