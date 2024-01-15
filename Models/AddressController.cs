using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BookStore.MVC.Models;
using System.Threading.Tasks;

public class AddressController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BookstoreContext _context;

    public AddressController(UserManager<ApplicationUser> userManager, BookstoreContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public IActionResult AddAddress()
    {
        return View("AddAddress");
    }

    [HttpPost]
    public async Task<ActionResult> AddAddress(Address address)
    {
        if (ModelState.IsValid)
        {
            // Pobierz zalogowanego użytkownika
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Przypisz Id zalogowanego użytkownika do UserId w adresie
                address.UserId = user.Id;

                // Dodaj nowy rekord do tabeli Address
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); // Przekieruj gdziekolwiek chcesz po dodaniu adresu
            }
            else
            {
                // Obsłuż błąd braku zalogowanego użytkownika
                return RedirectToAction("Login", "Account");
            }
        }

        return View(address);
    }
}