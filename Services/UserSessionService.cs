using EventEase.Models;
using EventEase.Extensions;
using Microsoft.JSInterop;

namespace EventEase.Services;

public class UserSessionService
{
    private const string StorageKey = "EventEase_UserSession";
    private readonly IJSRuntime _jsRuntime;
    private UserSession _session;
    private bool _initialized = false;

    public event Action? OnSessionChanged;

    public UserSessionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _session = new UserSession();
    }

    public async Task InitializeAsync()
    {
        if (_initialized) return;

        // Try to load existing session from localStorage
        var storedSession = await _jsRuntime.GetFromLocalStorageAsync<UserSession>(StorageKey);
        
        if (storedSession != null)
        {
            // Check if session is still valid (e.g., within 24 hours)
            var sessionAge = DateTime.Now - storedSession.LastActivity;
            if (sessionAge.TotalHours < 24)
            {
                _session = storedSession;
                _session.UpdateActivity();
            }
            else
            {
                // Session expired, create new
                _session = new UserSession();
            }
        }
        
        _initialized = true;
        await SaveSessionAsync();
    }

    public UserSession GetSession()
    {
        return _session;
    }

    public async Task UpdateActivityAsync()
    {
        _session.UpdateActivity();
        await SaveSessionAsync();
        OnSessionChanged?.Invoke();
    }

    public async Task AddRecentlyViewedEventAsync(int eventId)
    {
        _session.AddRecentlyViewedEvent(eventId);
        await SaveSessionAsync();
        OnSessionChanged?.Invoke();
    }

    public List<int> GetRecentlyViewedEventIds()
    {
        return _session.RecentlyViewedEventIds;
    }

    public async Task SetCustomDataAsync(string key, object value)
    {
        _session.CustomData[key] = value;
        _session.UpdateActivity();
        await SaveSessionAsync();
        OnSessionChanged?.Invoke();
    }

    public T? GetCustomData<T>(string key)
    {
        if (_session.CustomData.TryGetValue(key, out var value))
        {
            return (T)value;
        }
        return default;
    }

    public async Task ClearSessionAsync()
    {
        _session = new UserSession();
        await _jsRuntime.RemoveFromLocalStorageAsync(StorageKey);
        OnSessionChanged?.Invoke();
    }

    private async Task SaveSessionAsync()
    {
        await _jsRuntime.SetInLocalStorageAsync(StorageKey, _session);
    }
}
