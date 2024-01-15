using Microsoft.AspNetCore.Mvc;
using BookStore.MVC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Security.Claims;

public class AddressController : Controller
{
    private readonly BookstoreContext _context;


    public AddressController(BookstoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult AddAddress()
    {
        // Zwróć widok do dodawania adresu
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddAddress(Address Address)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Pobierz ID użytkownika z Claimów
                string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Sprawdź czy udało się pobrać ID użytkownika
                if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId))
                {
                    // Utwórz obiekt Address na podstawie danych z formularza
                    var newAddress = new Address
                    {
                        UserId = userId.ToString(),
                        Street = Address.Street,
                        Number = Address.Number,
                        Postcode = Address.Postcode,
                        City = Address.City,
                        // inne właściwości adresu
                    };

                    // Dodaj adres do bazy danych
                    _context.Addresses.Add(newAddress);
                    await _context.SaveChangesAsync();

                    // Przekieruj użytkownika gdzie chcesz po dodaniu adresu
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Błąd podczas przetwarzania ID użytkownika.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Błąd podczas dodawania adresu: {ex.Message}");
            }
        }

        // Jeśli walidacja nie powiodła się, zwróć użytkownika do widoku z błędami
        return View(Address);
    }


}
