﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BookStore.MVC.Models;
using System.Linq;
using BookstoreMVC.Models.Entities;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

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
    public IActionResult DodajDoKoszyka(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }

        // Odczytaj koszyk z sesji, jeśli istnieje, lub utwórz nowy
        var koszyk = HttpContext.Session.GetString("Koszyk") != null
            ? JsonSerializer.Deserialize<Koszyk>(HttpContext.Session.GetString("Koszyk"))
            : new Koszyk();

        // Dodaj wybraną książkę do koszyka
        koszyk.DodajDoKoszyka(book);

        // Zapisz koszyk z powrotem do sesji
        HttpContext.Session.SetString("Koszyk", JsonSerializer.Serialize(koszyk));

        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpPost]
    public IActionResult UsunZKoszyka(int bookId)
    {
        var koszyk = HttpContext.Session.GetString("Koszyk") != null
            ? JsonSerializer.Deserialize<Koszyk>(HttpContext.Session.GetString("Koszyk"))
            : new Koszyk();

        koszyk.UsunZKoszyka(bookId);

        HttpContext.Session.SetString("Koszyk", JsonSerializer.Serialize(koszyk));

        return RedirectToAction(nameof(Index), "Home");
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddBook(Book newBook)
    {
        if (ModelState.IsValid)
        {
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        return View(newBook);
    }
    [HttpPost]
    public async Task<IActionResult> PrzetworzZamowienie()
    {

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            // Dodatkowe zabezpieczenie na wypadek, gdyby użytkownik został wylogowany w momencie przetwarzania żądania
            return RedirectToAction("Login", "Identity Account");
        }
        var koszyk = HttpContext.Session.GetString("Koszyk") != null
            ? JsonSerializer.Deserialize<Koszyk>(HttpContext.Session.GetString("Koszyk"))
            : null;

        if (koszyk == null || !koszyk.Pozycje.Any())
        {
            return RedirectToAction("Index", "Home");
        }

        var zamowienie = new Order
        {
            UserId = userId,
            Price = koszyk.CenaCalkowitaKoszyka,
            Date = DateTime.Now,
            Status = "oczekujący",
            // inne właściwości zamówienia
        };

        _context.Orders.Add(zamowienie);
        await _context.SaveChangesAsync();

        HttpContext.Session.Remove("Koszyk");

        return RedirectToAction("Index", "Home");
    }


    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }

}
