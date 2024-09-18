using ContactManager.Services.ContactService;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers;

public sealed class ContactController(IContactService contactService)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await contactService.GetAllAsync());
    }
}