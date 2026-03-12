using System.Collections.Concurrent;
using System.Net;

namespace API.Middleware;

public class RateLimitingMiddleware(ILogger<RateLimitingMiddleware> logger) : IMiddleware
{
    private static readonly ConcurrentDictionary<string, (DateTime FirstRequest, int Count)> _ipRequests = new();
    private const int MaxRequestsPerMinute = 60;
    private const int MaxLoginAttemptsPerMinute = 5;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var path = context.Request.Path.ToString().ToLower();

        // Apply stricter limits to authentication endpoints
        var maxRequests = path.Contains("/login") || path.Contains("/register")
            ? MaxLoginAttemptsPerMinute
            : MaxRequestsPerMinute;

        var key = $"{ipAddress}:{path}";

        // Clean up old entries
        CleanupOldEntries();

        if (_ipRequests.TryGetValue(key, out var requestInfo))
        {
            var timeElapsed = DateTime.UtcNow - requestInfo.FirstRequest;

            if (timeElapsed.TotalMinutes < 1)
            {
                if (requestInfo.Count >= maxRequests)
                {
                    logger.LogWarning("Rate limit exceeded for {IpAddress} on {Path}", ipAddress, path);
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                    return;
                }

                _ipRequests[key] = (requestInfo.FirstRequest, requestInfo.Count + 1);
            }
            else
            {
                // Reset counter after 1 minute
                _ipRequests[key] = (DateTime.UtcNow, 1);
            }
        }
        else
        {
            _ipRequests[key] = (DateTime.UtcNow, 1);
        }

        await next(context);
    }

    private static void CleanupOldEntries()
    {
        var keysToRemove = _ipRequests
            .Where(kvp => (DateTime.UtcNow - kvp.Value.FirstRequest).TotalMinutes > 5)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var key in keysToRemove)
        {
            _ipRequests.TryRemove(key, out _);
        }
    }
}
