using ContactManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Contact> Contacts { get; init; }
}