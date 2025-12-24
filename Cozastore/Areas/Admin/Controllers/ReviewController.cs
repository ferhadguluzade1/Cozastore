using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cozastore.Areas.Admin.ViewModels.Review;
using Cozastore.DAL;
using Cozastore.Models;

namespace Cozastore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        CozastoreDB _db;
        public ReviewController(CozastoreDB db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var review = _db.Reviews
            .ToList();
            return View(review);
        }

        public IActionResult Create()
        {
            ViewBag.Products = _db.Products.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = _db.Products.ToList();
                return View(reviewViewModel);
            }

            Review review = new Review
            {
                Comment = reviewViewModel.Comment,
                ProductId = reviewViewModel.ProductId
            };

            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return View("Error");
            var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return View("Error");
            review.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return View("Error");
            var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return View("Error");
            review.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return View("Error");
            var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return View("Error");
            ViewBag.Products = _db.Products.ToList();
            UpdateReviewViewModel reviewViewModel = new UpdateReviewViewModel
            {
                Id = review.Id,
                Comment = review.Comment,
                ProductId = review.ProductId
            };
            return View(reviewViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateReviewViewModel reviewViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Products = _db.Products.ToList();
                return View(reviewViewModel);
            }

            var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == reviewViewModel.Id);
            if (review == null) return View("Error");
            review.Comment = reviewViewModel.Comment;
            review.ProductId = reviewViewModel.ProductId;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        
        }
    }
}