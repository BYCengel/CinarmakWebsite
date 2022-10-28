using Microsoft.AspNetCore.Mvc;

namespace WebsiteUI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
