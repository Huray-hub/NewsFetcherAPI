using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFetcherAPI.Data;

namespace NewsFetcherAPI.Service
{
    public interface INfDbService
    {
        Task<List<Category>> GetCategories();
    }
}