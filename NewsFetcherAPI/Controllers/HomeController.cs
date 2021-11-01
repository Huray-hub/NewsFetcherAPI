using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsFetcherAPI.Service;

namespace NewsFetcherAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkflowService _workflowService;

        public HomeController(IWorkflowService workflowService) => 
            _workflowService = workflowService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _workflowService.GetCategories();
            return View(model);
        }
        
        [HttpGet]
        public async Task<JsonResult> FetchNews(string category)
        {
            var model = await _workflowService.GetArticlesByCategory(category);
            return new JsonResult(model);
        }
    }
}