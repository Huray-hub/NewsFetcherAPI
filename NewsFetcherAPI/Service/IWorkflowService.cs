using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFetcherAPI.ViewModels;

namespace NewsFetcherAPI.Service
{
    public interface IWorkflowService
    {
         Task<List<CategoryModel>> GetCategories();
         Task<List<ArticleModel>> GetArticlesByCategory(string category);
    }
}