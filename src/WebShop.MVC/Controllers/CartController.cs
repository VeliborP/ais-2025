using Microsoft.AspNetCore.Mvc;

namespace WebShop.MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            //implementirati dodavanje u korpu
            return Ok();
        }
    }
}
