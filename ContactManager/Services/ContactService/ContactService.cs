using System.Globalization;
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
            throw new Exception("An error occurred while updating the contact.");
        }
    }

    public async Task ImportCsvAsync(IFormFile file)
    {
        if (file == null || file.Length == 0) throw new ArgumentException("The uploaded file is empty.");

        var extension = Path.GetExtension(file.FileName);

        if (string.IsNullOrWhiteSpace(extension) || !extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid file format. Please upload a CSV file.");

        var contacts = new List<Contact>();

        using var stream = new StreamReader(file.OpenReadStream());

        var isFirstLine = true;

        while (await stream.ReadLineAsync() is { } line)
        {
            if (isFirstLine)
            {
                isFirstLine = false;
                continue;
            }

            var values = line.Split(',');

            if (values.Length != 5) throw new ArgumentException("Invalid CSV format.");

            contacts.Add(new Contact
            {
                Name = values[0],
                DateOfBirth = DateOnly.ParseExact(values[1], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Married = bool.Parse(values[2]),
                Phone = values[3],
                Salary = decimal.Parse(values[4])
            });
        }

        try
        {
            await db.Contacts.AddRangeAsync(contacts);
            await db.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("An error occurred while importing the CSV file.");
        }
    }
}