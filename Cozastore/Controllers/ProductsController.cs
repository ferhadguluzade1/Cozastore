using Microsoft.AspNetCore.Mvc;
using Cozastore.DAL;
namespace Cozastore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CozastoreDB _cozastoreDB;
        public ProductsController(CozastoreDB cozastoreDB)
        {
            _cozastoreDB = cozastoreDB;
        }


        public IActionResult Index()
        {
            var data = _cozastoreDB.Products.ToList();
            return View(data);
        }
    }
}
