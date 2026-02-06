namespace EventEase.Models;

public class UserSession
{
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
    public DateTime SessionStartTime { get; set; } = DateTime.Now;
    public DateTime LastActivity { get; set; } = DateTime.Now;
    public List<int> RecentlyViewedEventIds { get; set; } = new();
    public Dictionary<string, object> CustomData { get; set; } = new();
    
    public void UpdateActivity()
    {
        LastActivity = DateTime.Now;
    }
    
    public void AddRecentlyViewedEvent(int eventId)
    {
        RecentlyViewedEventIds.Remove(eventId); // Remove if exists
        RecentlyViewedEventIds.Insert(0, eventId); // Add to front
        
        // Keep only last 10
        if (RecentlyViewedEventIds.Count > 10)
        {
            RecentlyViewedEventIds = RecentlyViewedEventIds.Take(10).ToList();
        }
        
        UpdateActivity();
    }
    
    public TimeSpan GetSessionDuration()
    {
        return DateTime.Now - SessionStartTime;
    }
}
