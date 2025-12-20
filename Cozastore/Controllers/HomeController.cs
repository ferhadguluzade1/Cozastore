using Cozastore.DAL;
using Cozastore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cozastore.Controllers
{
    public class HomeController : Controller
    {
        private readonly CozastoreDB _context;
        public HomeController(CozastoreDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var sliders = _context.Sliders.OrderBy(x => x.Order).ToList();
            var categories = _context.Categories.ToList();
            var products = _context.Products.ToList();

            var model = new HomeViewModel
            {
                Sliders = sliders,
                Categories = categories,
                Products = products
            };
            return View(model);
        }
    }
}
