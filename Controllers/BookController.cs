using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BookStore.MVC.Models;
using System.Linq;

public class BookController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly BookstoreContext _context;

    public BookController(IConfiguration configuration, BookstoreContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Book book = _context.Books.Find(id);

        if (book == null)
        {
            return NotFound();
        }

        return View("Edit",book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Category,Title,Author,Publisher,Year,Pages,Price,Description,Img")] Book updatedBook)
    {
        if (id != updatedBook.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(updatedBook).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(updatedBook.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        return View(updatedBook);
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
