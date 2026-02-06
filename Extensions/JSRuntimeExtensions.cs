using Microsoft.JSInterop;
using System.Text.Json;

namespace EventEase.Extensions;

public static class JSRuntimeExtensions
{
    public static async Task<T?> GetFromLocalStorageAsync<T>(this IJSRuntime jsRuntime, string key)
    {
        try
        {
            var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }

    public static async Task SetInLocalStorageAsync<T>(this IJSRuntime jsRuntime, string key, T value)
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }
        catch
        {
            // Silently fail if localStorage is not available
        }
    }

    public static async Task RemoveFromLocalStorageAsync(this IJSRuntime jsRuntime, string key)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
        catch
        {
            // Silently fail
        }
    }
}
