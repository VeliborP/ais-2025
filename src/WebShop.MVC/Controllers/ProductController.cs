using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.BLL.Interfaces;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService service, ICategoryService categoryService)
        {
            _productService = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            var vm = products.Select(ProductViewModel.FromEntity).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            return View(ProductViewModel.FromEntity(product));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();

            var productViewModel = ProductViewModel.FromEntity(new DAL.Models.Product() { Code = "", Name = "" }, categories);

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string? imagePath = null;

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                // serverska validacija, samo fajlovi sa doazvoljenim ekstenzijama
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(vm.ImageFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    var categories = await _categoryService.GetAllAsync();
                    vm.Categories = categories.Select(model => new SelectListItem
                    {
                        Value = model.Id.ToString(),
                        Text = model.Code + "-" + model.Name,
                        Selected = model.Id == vm.CategoryId
                    }).ToList();

                    ModelState.AddModelError("ImageFile", "Only JPG, PNG, GIF images are allowed.");
                    return View(vm);
                }

                // ograničenje veličine fajla
                if (vm.ImageFile.Length > 5_000_000) // 5 MB
                {
                    var categories = await _categoryService.GetAllAsync();
                    vm.Categories = categories.Select(model => new SelectListItem
                    {
                        Value = model.Id.ToString(),
                        Text = model.Code + "-" + model.Name,
                        Selected = model.Id == vm.CategoryId
                    }).ToList();

                    ModelState.AddModelError("ImageFile", "File size must be less than 5 MB.");
                    return View(vm);
                }

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            //prosledjivanje putanju ka slici na entity model
            var entity = vm.ToEntity(imagePath);

            await _productService.CreateAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var categories = await _categoryService.GetAllAsync();
            var productViewModel = ProductViewModel.FromEntity(product, categories);

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            string? imagePath = vm.ImagePath; // zadrži staru sliku

            // Ako se izabere brisanje slike
            if (vm.DeleteImage && !string.IsNullOrEmpty(vm.ImagePath))
            {
                string oldPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    vm.ImagePath.TrimStart('/')
                );

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                imagePath = null;
            }

            // Ako je uploadovana nova slika
            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                // serverska validacija, samo fajlovi sa doazvoljenim ekstenzijama
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(vm.ImageFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    var categories = await _categoryService.GetAllAsync();
                    vm.Categories = categories.Select(model => new SelectListItem
                    {
                        Value = model.Id.ToString(),
                        Text = model.Code + "-" + model.Name,
                        Selected = model.Id == vm.CategoryId
                    }).ToList();

                    ModelState.AddModelError("ImageFile", "Only JPG, PNG, GIF images are allowed.");
                    return View(vm);
                }

                // ograničenje veličine fajla
                if (vm.ImageFile.Length > 5_000_000) // 5 MB
                {
                    var categories = await _categoryService.GetAllAsync();
                    vm.Categories = categories.Select(model => new SelectListItem
                    {
                        Value = model.Id.ToString(),
                        Text = model.Code + "-" + model.Name,
                        Selected = model.Id == vm.CategoryId
                    }).ToList();

                    ModelState.AddModelError("ImageFile", "File size must be less than 5 MB.");
                    return View(vm);
                }

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            // Kreiramo entity sa novom/postaćom slikom
            var entity = vm.ToEntity(imagePath, vm.DeleteImage);

            // Update preko servisa
            await _productService.UpdateAsync(entity);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            return View(ProductViewModel.FromEntity(product));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
