using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShop.BLL.Interfaces;
using WebShop.DAL.Models;
using WebShop.MVC.Models;
using WebShop.MVC.ViewModels.Home;

namespace WebShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _productService.GetCategoriesWithProductsAsync();
            var viewModel = IndexViewModel.FromEntities(categories);

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            List<StudentViewModel> studentList = new List<StudentViewModel>
            {
                new StudentViewModel
                {
                    Id = 1,
                    Name = "Marko Markovic",
                    IdNumber = "13/2022"
                },
                new StudentViewModel
                {
                    Id = 2,
                    Name = "Nikola Nikolic",
                    IdNumber = "14/2022"
                },
                new StudentViewModel
                {
                    Id = 3,
                    Name = "Sima Simic",
                    IdNumber = "15/2022"
                }
            };


            return View(studentList);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
