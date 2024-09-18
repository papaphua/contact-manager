namespace ContactManager.Entities;

public sealed class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool Married { get; set; }

    public string Phone { get; set; }

    public decimal Salary { get; set; }
}