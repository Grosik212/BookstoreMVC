using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(string category)
        {
            // Tutaj możesz dodać logikę obsługi kategorii, jeśli jest to wymagane
            return View();
        }

        public ActionResult OrderStatus()
        {
            // Tutaj możesz dodać logikę obsługi statusu zamówienia, jeśli jest to wymagane
            return View();
        }
    }
}
