using BookStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace BookStore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookstoreContext _context;

        public HomeController(BookstoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()

        {
            
            var books = _context.Books.ToList();

            return View(books);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
