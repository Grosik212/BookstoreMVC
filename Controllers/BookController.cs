using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BookStore.MVC.Models;

public class BookController : Controller
{
    private readonly BookstoreContext _context;

    public BookController(BookstoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
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
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
        }

        // Jeśli ModelState.IsValid jest false, wróć do widoku edycji z błędami walidacji
        return View(updatedBook);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
