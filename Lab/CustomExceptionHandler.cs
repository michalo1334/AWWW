using Microsoft.AspNetCore.Diagnostics;

public class CustomExceptionHandler : IExceptionHandler
{
public CustomExceptionHandler(ILogger<CustomExceptionHandler> log)
{
Log = log;
}
public ILogger<CustomExceptionHandler> Log { get; }
public ValueTask<bool> TryHandleAsync(HttpContext httpContext,
Exception exception, CancellationToken cancellationToken)
{
Log.LogCritical(exception, "Unhandled Error: ");
httpContext.Response.StatusCode = 500;
httpContext.Features.Set<Exception>(exception);
return ValueTask.FromResult(false);
}
}