using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingWebAPI.DAL.Entities;

namespace WebPages.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public CategoriesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7067/api/Categories/Get";
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var url = "https://localhost:7067/api/Categories/Create";
            await _httpClient.CreateClient().PostAsJsonAsync(url, category);
            return RedirectToAction("Index");
        }
    }
}
