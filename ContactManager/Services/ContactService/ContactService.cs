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

    public async Task DeleteAsync(Guid id)
    {
        var contact = await db.Contacts
            .FirstOrDefaultAsync(contact => contact.Id == id);

        if (contact == null) throw new KeyNotFoundException($"Contact with id {id} was not found.");

        try
        {
            db.Remove(contact);
            await db.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while deleting the contact.");
        }
    }

    public async Task UpdateAsync(Guid id, Contact updatedContact)
    {
        var contact = await db.Contacts
            .FirstOrDefaultAsync(contact => contact.Id == id);

        if (contact == null) throw new KeyNotFoundException($"Contact with id {id} was not found.");
        
        contact.Name = updatedContact.Name;
        contact.DateOfBirth = updatedContact.DateOfBirth;
        contact.Married = updatedContact.Married;
        contact.Phone = updatedContact.Phone;
        contact.Salary = updatedContact.Salary;
        
        try
        {
            await db.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while deleting the contact.");
        }
    }
}