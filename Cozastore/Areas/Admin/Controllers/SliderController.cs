using Cozastore.DAL;
using Cozastore.Models;
using Microsoft.AspNetCore.Mvc;
using Cozastore.Utilities.ImageUpload;

namespace Cozastore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly CozastoreDB _db;
        private readonly IWebHostEnvironment _env;

        public SliderController(CozastoreDB context, IWebHostEnvironment env)
        {
            _db = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<Slider> sliders = _db.Sliders
                .ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!slider.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "File type must be image");
                return View();
            }
            if(slider.ImageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "Image size must be max 2MB");
                return View();
            }
            var fileName = slider.ImageFile.SaveImage(_env, "Uploads/Slider");
            slider.ImageUrl = fileName;
            if (!ModelState.IsValid) return View();
            if (slider == null) return View("Error");
            _db.Sliders.Add(slider);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) return View("Error");
            Slider? slider = _db.Sliders.FirstOrDefault(item => item.Id == id);
            if (slider == null) return View("Error");
            slider.IsDeleted = true;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Restore(int? id)
        {
            if (id == null) return View("Error");
            Slider? slider = _db.Sliders.FirstOrDefault(item => item.Id == id);
            if (slider == null) return View("Error");
            slider.IsDeleted = false;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return View("Error");
            Slider? slider = _db.Sliders.FirstOrDefault(item => item.Id == id);
            if (slider == null) return View("Error");
            return View(slider);
        }

        [HttpPost]
        public IActionResult Update(Slider newSlider)
        {
            if (!ModelState.IsValid) return View();
            if (newSlider == null) return View("Error");
            Slider? oldSlider = _db.Sliders.FirstOrDefault(item => item.Id == newSlider.Id);
            if (oldSlider == null) return View("Error");

            oldSlider.Title = newSlider.Title;
            oldSlider.Description = newSlider.Description;
            oldSlider.ImageUrl = newSlider.ImageUrl;
            oldSlider.DiscountRate = newSlider.DiscountRate;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}