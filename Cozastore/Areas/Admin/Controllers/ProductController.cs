using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cozastore.Areas.Admin.ViewModels.Product;
using Cozastore.DAL;
using Cozastore.Models;

namespace Cozastore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        CozastoreDB _db;
        public ProductController(CozastoreDB db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            List<Product> products = _db.Products
                .Include(c => c.Categories)
                .Include(t => t.Tags)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Tags = _db.Tags.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productViewModel)
        {
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Tags = _db.Tags.ToList();
            if (!ModelState.IsValid) return View();
            if (productViewModel is null) return View("Error");

            Product product = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Rating = productViewModel.Rating,
                SKU = productViewModel.SKU
            };

            if(productViewModel.CategoryIds != null)
            {
                product.Categories = _db.Categories
                    .Where(c => productViewModel.CategoryIds.Contains(c.Id))
                    .ToList();
            }

            if(productViewModel.TagIds != null)
            {
                product.Tags = _db.Tags
                    .Where(t => productViewModel.TagIds.Contains(t.Id))
                    .ToList();
            }
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return View("Error");
            Product? product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            product.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id is null) return View("Error");
            Product? product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            product.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Tags = await _db.Tags.ToListAsync();

            Product? product = await _db.Products
                .Include(c => c.Categories)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return View("Error");

            UpdateProductViewModel productViewModel = new UpdateProductViewModel
            {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                Price=product.Price,
                CategoryIds=product.Categories.Select(c=>c.Id).ToList(),
                TagIds = product.Tags.Select(t=>t.Id).ToList()

            };
            return View(productViewModel);
        }

        public async Task<IActionResult> Update(UpdateProductViewModel productViewModel)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Tags = await _db.Tags.ToListAsync();

            if (!ModelState.IsValid) return View();

            Product? existProduct = await _db.Products
                .Include(c => c.Categories)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(p => p.Id == productViewModel.Id);

            if (existProduct is null) return View("Error");

            existProduct.Name = productViewModel.Name;
            existProduct.Description = productViewModel.Description;
            existProduct.Price = productViewModel.Price;
            existProduct.Categories.Clear();

            if (productViewModel.CategoryIds != null)
            {
                existProduct.Categories = _db.Categories
                    .Where(c => productViewModel.CategoryIds.Contains(c.Id))
                    .ToList();
            }

            existProduct.Tags.Clear();
            if(productViewModel.TagIds != null)
            {
                existProduct.Tags = _db.Tags
                    .Where(t => productViewModel.TagIds.Contains(t.Id))
                    .ToList();
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
