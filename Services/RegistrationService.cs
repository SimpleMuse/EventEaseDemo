using EventEase.Models;

namespace EventEase.Services;

public class Registration
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int EventId { get; set; }
}

public class RegistrationService
{
    private readonly List<Registration> registrations = new();
    private int nextId = 1;
    public event Action? RegistrationsChanged;

    public Registration Add(string name, string email, int eventId)
    {
        var reg = new Registration
        {
            Id = nextId++,
            Name = name,
            Email = email,
            EventId = eventId
        };
        registrations.Add(reg);
        RegistrationsChanged?.Invoke();
        return reg;
    }

    public List<Registration> GetAll()
    {
        return new List<Registration>(registrations);
    }

    public List<Registration> GetByEventId(int eventId)
    {
        return registrations.Where(r => r.EventId == eventId).ToList();
    }
}
