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

        public IActionResult BookCard(int id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        private List<Book> GetBooksByCategory(string categoryName)
        {
            var books = _context.Books.ToList();
            var booksInCategory = books.Where(book => book.Category == categoryName).ToList();

            return booksInCategory;
        }

        public IActionResult CategoryCard(string categoryName)
        {
            var books = GetBooksByCategory(categoryName);
            return View(books);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult basketCard()
        {
            return View();
        }
    }
}
