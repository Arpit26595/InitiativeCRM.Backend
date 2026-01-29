namespace Lead.Domain.Entities;

public class Leads
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private Leads() { } // EF

    public Leads(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }
}


