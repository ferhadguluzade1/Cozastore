using Cozastore.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Cozastore.Controllers
{
    public class HomeController : Controller
    {
        CozastoreDB _context;
        public HomeController(CozastoreDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
