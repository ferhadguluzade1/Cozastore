using Microsoft.AspNetCore.Mvc;
using Cozastore.DAL;
using Cozastore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Cozastore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CozastoreDB _cozastoreDB;
        public ProductsController(CozastoreDB cozastoreDB)
        {
            _cozastoreDB = cozastoreDB;
        }


        public IActionResult Detail(int id)
        {
            var product = _cozastoreDB.Products.Include(x=>x.Category).FirstOrDefault(x=>x.Id==id);
            if (product == null) return NotFound();

            var related = _cozastoreDB.Products
                                      .Where(x => x.CategoryId == product.CategoryId && x.Id != id)
                                      .Take(4)
                                      .ToList();
            if (product == null) return NotFound();

            
            var vm = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = related
            };

            return View(vm);
        }
    }
}
