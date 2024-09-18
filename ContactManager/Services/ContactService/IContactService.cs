using ContactManager.Entities;

namespace ContactManager.Services.ContactService;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();
}