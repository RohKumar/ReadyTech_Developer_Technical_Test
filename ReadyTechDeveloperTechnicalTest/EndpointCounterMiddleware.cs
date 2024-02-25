using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

/// <summary>
/// Middleware will check for count of requested end point, if count is in multiple of 5 will return 503 Service Unavailable.
/// </summary>
public class EndpointCounterMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly Dictionary<string, int> _endPointRequest = new Dictionary<string, int>();

    public EndpointCounterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.Request.Path;

        _= _endPointRequest.ContainsKey(context.Request.Path) ? _endPointRequest[context.Request.Path]++ : _endPointRequest[context.Request.Path] = 1;

        int requestCount;
        _endPointRequest.TryGetValue(endpoint, out requestCount);
        if (requestCount % 5 == 0)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("503 Service Unavailable");
                
            //Stop further processing and returns the response
            return;
        }

        await _next(context);
    }
}

public static class EndpointCounterMiddlewareExtensions
{
    public static IApplicationBuilder UseEndpointCounterMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<EndpointCounterMiddleware>();
    }
}