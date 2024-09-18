using ContactManager.Entities;

namespace ContactManager.Services.ContactService;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();

    Task DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, Contact updatedContact);
}