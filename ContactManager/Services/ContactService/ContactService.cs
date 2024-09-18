using ContactManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services.ContactService;

public sealed class ContactService(ApplicationDbContext db)
    : IContactService
{
    public async Task<List<Contact>> GetAllAsync()
    {
        return await db.Contacts.ToListAsync();
    }
}