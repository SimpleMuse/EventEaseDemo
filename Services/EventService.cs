using EventEase.Models;

namespace EventEase.Services;

public class EventService
{
    private List<Event> events;

    public EventService()
    {
        events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Name = "Tech Conference 2026",
                Date = new DateTime(2026, 3, 15),
                Location = "New York, NY",
                Description = "Annual technology conference featuring industry leaders and cutting-edge innovations. Network with professionals, attend keynote speeches, and explore the latest tech trends."
            },
            new Event
            {
                Id = 2,
                Name = "Webinar: AI & Machine Learning",
                Date = new DateTime(2026, 2, 20),
                Location = "Virtual",
                Description = "Learn the latest trends in AI and machine learning from industry experts. This webinar covers practical applications, best practices, and future opportunities in AI."
            },
            new Event
            {
                Id = 3,
                Name = "Networking Mixer",
                Date = new DateTime(2026, 2, 28),
                Location = "San Francisco, CA",
                Description = "Connect with professionals in the tech industry over refreshments and casual conversations. Perfect opportunity to expand your professional network and discover new career opportunities."
            },
            new Event
            {
                Id = 4,
                Name = "Web Development Workshop",
                Date = new DateTime(2026, 4, 5),
                Location = "Austin, TX",
                Description = "Hands-on workshop covering modern web development practices. Learn responsive design, JavaScript frameworks, and best practices for building scalable web applications."
            },
            new Event
            {
                Id = 5,
                Name = "Cloud Computing Bootcamp",
                Date = new DateTime(2026, 3, 22),
                Location = "Seattle, WA",
                Description = "Intensive training on cloud infrastructure and deployment. Master cloud platforms, containerization, and DevOps practices with real-world projects and hands-on exercises."
            }
        };
    }

    public List<Event> GetAllEvents()
    {
        return new List<Event>(events);
    }

    public Event GetEventById(int id)
    {
        return events.FirstOrDefault(e => e.Id == id);
    }
}
