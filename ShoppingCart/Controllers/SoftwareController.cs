using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class SoftwareController : Controller
    {
        

        public IActionResult Index()
        {
            Data data = new Data();
            ViewBag.CartCount = GetCartCount();
            return View(data.softwares);
        }

        public IActionResult Reviews()
        {
            return View();
        }

        public IActionResult Search(string searchString)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int softwareId)
        {


            
            int cartCount = GetCartCount();
            cartCount++;
            HttpContext.Session.SetInt32("CartCount", cartCount);

           
            return RedirectToAction("ViewCart", "Purchase");

            
        }

        private int GetCartCount()
        {
            return HttpContext.Session.GetInt32("CartCount") ?? 0;
        }
    }
}
