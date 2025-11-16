using Microsoft.AspNetCore.Mvc;
using WebShop.BLL.Interfaces;
using WebShop.DAL.Models;
using WebShop.MVC.ViewModels;

namespace WebShop.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            var vm = categories.Select(CategoryViewModel.FromEntity).ToList();
            return View(vm);
        }

        // GET: /Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();

            return View(CategoryViewModel.FromEntity(category));
        }

        // GET: /Category/Create
        public IActionResult Create() => View();

        // POST: /Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _service.CreateAsync(vm.ToEntity());
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();

            return View(CategoryViewModel.FromEntity(category));
        }

        // POST: /Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            await _service.UpdateAsync(vm.ToEntity());
            return RedirectToAction(nameof(Index));
        }

        // GET: /Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();

            return View(CategoryViewModel.FromEntity(category));
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
