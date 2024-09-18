using ContactManager.Entities;
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

    public async Task<IActionResult> Delete(Guid id)
    {
        await contactService.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(Guid id, Contact updatedContact)
    {
        await contactService.UpdateAsync(id, updatedContact);
        return RedirectToAction("Index");
    }
}