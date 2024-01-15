using Microsoft.AspNetCore.Mvc;
using BookStore.MVC.Models;

public class AddressController : Controller
{
    public IActionResult AddAddress()
    {
        return View("AddAddress");
    }

    [HttpPost]
    public IActionResult AddAddress(Address address)
    {
        // Tutaj możesz obsłużyć zapisanie adresu w bazie danych
        // Następnie przekieruj użytkownika, gdzie chcesz
        return RedirectToAction("Index", "Home");
    }
}
